using lapr5_masterdata_viagens.Domain.Shared;
using System;

namespace lapr5_masterdata_viagens.Domain.Drivers
{
    public class DriverId : EntityId
    {
        public DriverId(string value) : base(value)
        {
        }

        override
        protected Object createFromString(String text)
        {
            if (text == null)
                return Guid.NewGuid();

            return new Guid(text);
        }

        override
        public String AsString()
        {
            Guid obj = (Guid)base.ObjValue;
            return obj.ToString();
        }


        public Guid AsGuid()
        {
            return (Guid)base.ObjValue;
        }
    }
}