using GibddApp.Db;
using GibddApp.Db.Models;
using System.ComponentModel;

namespace GibddApp.Forms
{
    internal class ProtocolForm : FormBase
    {
        public ProtocolForm() : base(         
            Tables.Protocol,
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
            Text = "Протоколы";
        }

        protected override IBindingList? LoadData()
        {
            var protocols = Repository.GetProtocols();
            return new BindingList<Protocol>(protocols);
        }
    }
}
