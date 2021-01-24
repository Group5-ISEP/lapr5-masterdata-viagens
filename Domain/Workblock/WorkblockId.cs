using lapr5_masterdata_viagens.Domain.Shared;
using System;

namespace lapr5_masterdata_viagens.Domain.Workblocks
{
    public class WorkblockId : EntityId
    {
        public WorkblockId(string value) : base(value)
        {
        }

        override
        protected Object createFromString(String text)
        {
            if (text == null)
                return Guid.NewGuid().ToString();

            return text;
        }

        override
        public String AsString()
        {
            return this.Value;
        }
    }
}