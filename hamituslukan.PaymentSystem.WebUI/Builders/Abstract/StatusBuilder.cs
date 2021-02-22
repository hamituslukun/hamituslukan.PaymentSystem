using hamituslukan.PaymentSystem.Dto.Concrete;
using hamituslukan.PaymentSystem.WebUI.Builders.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hamituslukan.PaymentSystem.WebUI.Builders.Abstract
{
    public abstract class StatusBuilder
    {
        public abstract Status GenerateStatus(ApplicationUserDto activeUser, string roles);
    }
}