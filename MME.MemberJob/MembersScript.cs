using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MME.Data;
using MME.Model.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MME.MemberJob
{
    internal class MembersScript
    {
        int keySize = 64;
        int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        readonly MMEAppDBContext _context;

        public MembersScript(MMEAppDBContext context)
        {
            _context = context;
        }

        public void InsertMembers()
        {
            Byte[] PasswordSalt;
            var PasswordHash = HashPasword("123", out PasswordSalt);
            var user = _context.Users.FirstOrDefault();
            //if (user == null)
            //{
            List<string> _errormessages = new List<string>();
            var dtMembers = ReadExcelFile("F:\\Sachin_Work\\MyCommunity\\Sample.xlsx", out _errormessages);
            var members = ConvertToList<UserModel>(dtMembers);
            var start = 1;
            foreach (var member in members)
            {
                if (start >= 15)
                {
                    _context.Users.Add(new MME.Model.Shared.UserModel
                    {
                        Username = member.LastName.Substring(member.LastName.Length - 2, 2).ToString().ToUpper() + start.ToString("0000000"),
                        PasswordSalt = PasswordSalt,
                        Password = PasswordHash,
                        FirstName = member.FirstName,
                        MiddleName = member.MiddleName,
                        LastName = member.LastName,
                        RoleId = member.RoleId,
                        Mobile = member.Mobile,
                        Email = member.Email,
                        Gender = member.Gender,
                        MaritalStatus = member.MaritalStatus,
                        Society = member.Society,
                        Area = member.Area,
                        Location = member.Location,
                        Landmark = member.Landmark,
                        City = member.City,
                        PincodeId = member.PincodeId,
                        StateId = member.StateId,
                        BirthDate = null,
                        IsActive = true,
                    });
                }
                start++;
            }
            _context.SaveChanges();
            //}
            //else
            //{
            //    var start = 16;
            //    for (int i = 0; i < 20; i++)
            //    {
            //        for (int j = 0; j < 200; j++)
            //        {
            //            _context.Users.Add(new MME.Model.Shared.UserModel
            //            {
            //                Username = "Test" + start.ToString("000000"),
            //                PasswordSalt = PasswordSalt,
            //                Password = PasswordHash,
            //                FirstName = "Test",
            //                MiddleName = string.Empty,
            //                LastName = start.ToString("000000"),
            //                RoleId = 3,
            //                Mobile = start.ToString("00000000000"),
            //                Gender = "Male",
            //                MaritalStatus = "Married",
            //                BirthDate = DateTime.Now.AddYears(-20),
            //                IsActive = true,
            //            });
            //            start++;
            //        }
            //        _context.SaveChanges();
            //    }
            //}
        }

        string HashPasword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }

        private DataTable ReadExcelFile(string FileName, out List<string> ErrorMessages)
        {
            ErrorMessages = new List<string>();
            DataTable dataTable = new DataTable();

            // Open the Excel file in Read Mode using OpenXml.
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(FileName, false))
            {
                //Read the first Sheet from Excel file.
                Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();

                //Get the Worksheet instance.
                Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;

                //Fetch all the rows present in the Worksheet.
                IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();

                //Create a new DataTable.
                var RowIndex = 0;
                //Loop through the Worksheet rows.
                foreach (Row row in rows)
                {
                    RowIndex++;
                    //Use the first row to add columns to DataTable.
                    if (RowIndex == 1)
                    {
                        foreach (Cell cell in row.Descendants<Cell>())
                        {
                            dataTable.Columns.Add(GetValue(doc, cell));
                        }
                    }
                    else
                    {
                        //Add rows to DataTable.
                        dataTable.Rows.Add();
                        int i = 0;
                        foreach (Cell cell in SpreedsheetHelper.GetRowCells(row))
                        {
                            try
                            {
                                dataTable.Rows[dataTable.Rows.Count - 1][i] = GetValue(doc, cell);
                            }
                            catch (Exception ex)
                            {

                            }
                            i++;
                        }
                    }
                }
                return dataTable;

            }
        }


        private string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = string.Empty;
            if (cell.CellValue != null)
            {
                value = cell.CellValue.InnerText;
                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
                }
            }
            return value;
        }

        public List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName)
                    .ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    var column = columnNames.Where(c => c.Trim() == pro.Name).FirstOrDefault();
                    if (!string.IsNullOrEmpty(column) && column == "StateId")
                    {

                    }
                    //if (columnNames.Contains(pro.Name.Trim()))
                    if (column != null)
                    {
                        PropertyInfo pI = objT.GetType().GetProperty(pro.Name);
                        if (pI != null && pI.PropertyType.FullName == "System.Nullable`1[[System.Decimal, System.Private.CoreLib, Version=5.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]")
                        {
                            if (!string.IsNullOrEmpty(row[pro.Name].ToString()))
                            {
                                pro.SetValue(objT, row[column] == DBNull.Value ? null : Convert.ChangeType(row[column], typeof(decimal)));
                            }
                            else
                            {
                                pro.SetValue(objT, null);
                            }
                        }
                        else if (pI != null && pI.PropertyType.FullName == "System.Nullable`1[[System.Int32, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]")
                        {
                            if (!string.IsNullOrEmpty(row[pro.Name].ToString()))
                            {
                                pro.SetValue(objT, row[column] == DBNull.Value ? null : Convert.ChangeType(row[column], typeof(Int32)));
                            }
                            else
                            {
                                pro.SetValue(objT, null);
                            }
                        }
                        else if (pI != null && pI.PropertyType.FullName == "System.Nullable`1[[System.Int64, System.Private.CoreLib, Version=5.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]")
                        {
                            if (!string.IsNullOrEmpty(row[pro.Name].ToString()))
                            {
                                pro.SetValue(objT, row[column] == DBNull.Value ? null : Convert.ChangeType(row[column], typeof(Int64)));
                            }
                            else
                            {
                                pro.SetValue(objT, null);
                            }
                        }
                        else
                        {
                            try
                            {
                                pro.SetValue(objT, row[column] == DBNull.Value ? null : Convert.ChangeType(row[column], pI.PropertyType));
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
                return objT;
            }).ToList();
        }

    }
}
