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
                ("NoProtocol", "N протокола"),
                ("CodeVio", "Код нарушения"),
                ("DateVio", "Дата нарушения"),
                ("TimeVio", "Время нарушения"),
                ("License", "N вод. уд-я")
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
