using FirebirdSql.Data.FirebirdClient;
using GibddApp.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace GibddApp.Db
{
    internal class Repository
    {
        public List<Car> GetCars()
        {
            if (!LoginInfo.CanSelect(Tables.Car))
                return new List<Car>();

            try
            {
                using (var db = new GibddDbContext())
                {
                    var cars = db.Cars.OrderBy(c => c.Gosno).ToList();
                    return cars;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения списка автомобилей: {ex.Message} {ex.InnerException?.Message}");
                return new List<Car>();
            }
        }

        public List<Driver> GetDrivers()
        {
            if (!LoginInfo.CanSelect(Tables.Driver))
                return new List<Driver>();

            try
            {
                using (var db = new GibddDbContext())
                {
                    var drivers = db.Drivers.OrderBy(c => c.License).ToList();
                    return drivers.ToList();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка получения списка водителей: {ex.Message} {ex.InnerException?.Message}");
                return new List<Driver>();
            }            
        }
        
        public List<Protocol> GetProtocols()
        {
            if (!LoginInfo.CanSelect(Tables.Protocol))
                return new List<Protocol>();

            try
            {
                using (var db = new GibddDbContext())
                {
                    var protocol = db.Protocols.OrderBy(c => c.NoProtocol).ToList();
                    return protocol.ToList();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка получения списка протоколов: {ex.Message} {ex.InnerException?.Message}");
                return new List<Protocol>();
            }
        }
        
        public List<Violation> GetViolations()
        {
            if (!LoginInfo.CanSelect(Tables.Violation))
                return new List<Violation>();

            try
            {
                using (var db = new GibddDbContext())
                {
                    var violations = db.Violations.OrderBy(c => c.CodeVio).ToList();
                    return violations.ToList();                    
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка получения списка нарушений: {ex.Message} {ex.InnerException?.Message}");
                return new List<Violation>();
            }            
        }

        public List<UserPrivilege> GetAllUsers()
        {
            try
            {
                using (var db = new GibddDbContext())
                {
                    var violations = db.UserPrivileges.Where(c => c.UserName != "SYSDBA")
                        .Select(i => new UserPrivilege
                        {
                            TableName = i.TableName.Trim(),
                            UserName = i.UserName.Trim(),
                            Privilege = i.Privilege.Trim()
                        })
                        .ToList();

                    return violations.ToList();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка получения списка пользователей: {ex.Message} {ex.InnerException?.Message}");
                return new List<UserPrivilege>();
            }            
        }

        public List<UserPrivilege>? GetCurrentUserPrivileges()
        {
            try
            {
                using (var db = new GibddDbContext())
                {
                    var userPrivileges = db.UserPrivileges
                        .Where(c => c.UserName == LoginInfo.Login.Trim())
                        .ToList()
                        .Select(i => new UserPrivilege
                        {
                            UserName = i.UserName.Trim(),
                            TableName = i.TableName.Trim(),
                            Privilege = i.Privilege.Trim()
                        })
                        .ToList();

                    return userPrivileges;
                }
            }
            catch (FbException ex)
            {
                string errorMessage;
                if (ex.ErrorCode == 335544472)
                    errorMessage = "Логин пароль указаны неверно";
                else
                    errorMessage = $"{ex.Message} {ex.InnerException?.Message}";

                ShowError(errorMessage);
                return new List<UserPrivilege>();
            }            
        }

        public bool Update<T>(T item)
        {
            try
            {
                using (var db = new GibddDbContext())
                {
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }                
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка обновления записи:{ex.Message} {ex.InnerException?.Message}");
                return false;
            }

            return true;
        }

        public bool Add<T>(T item)
        {
            try
            {
                using (var db = new GibddDbContext())
                {
                    db.Add(item);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка добавления записи:{ex.Message} {ex.InnerException?.Message}");
                return false;
            }

            return true;
        }

        public bool Delete<T>(T item)
        {
            try
            {
                using (var db = new GibddDbContext())
                {
                    db.Remove(item);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка удаления записи:{ex.Message} {ex.InnerException?.Message}");
                return false;
            }

            return true;
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }        
    }
}
