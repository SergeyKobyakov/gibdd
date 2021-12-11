namespace GibddApp.Db.Models
{
    internal class UserPrivilege
    {
        public string UserName { get; set; }
        public string TableName { get; set;}
        public string Privilege { get; set; }
    }
}
