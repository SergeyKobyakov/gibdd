using GibddApp.Db;
using GibddApp.Db.Models;
using System.ComponentModel;

namespace GibddApp.Forms
{
    internal class ProtocolForm : FormBase
    {
        public ProtocolForm() : base(            
            new[]
            {
                "NoProtocol",
                "CodeVio",
                "DateVio",
                "TimeVio",
                "License"
            },
            new[]
            {
                "NoProtocol"
            })
        {
            Text = "Protocol";
        }

        protected override IBindingList? LoadData()
        {
            using (var db = new GibddDbContext())
            {
                var protocol = db.Protocols.OrderBy(c => c.NoProtocol).ToList();
                return new BindingList<Protocol>(protocol);
            }
        }
    }
}
