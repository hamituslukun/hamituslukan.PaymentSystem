using hamituslukan.PaymentSystem.Dto.Concrete;
using hamituslukan.PaymentSystem.WebUI.Builders.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hamituslukan.PaymentSystem.WebUI.Builders.Concrete
{
    public class Status
    {
        public bool AccessStatus { get; set; }
    }

    public class StatusBuilderDirector
    {
        private StatusBuilder builder;

        public StatusBuilderDirector(StatusBuilder builder)
        {
            this.builder = builder;
        }

        public Status GenerateStatus(ApplicationUserDto activeUser, string roles)
        {
            return builder.GenerateStatus(activeUser, roles);
        }
    }

    public class SingleRoleStatusBuilder : StatusBuilder
    {
        public override Status GenerateStatus(ApplicationUserDto activeUser, string roles)
        {
            Status status = new Status();
            if (activeUser.Roles.Contains(roles))
            {
                status.AccessStatus = true;
            }
            return status;
        }
    }

    public class MultiRoleStatusBuilder : StatusBuilder
    {
        public override Status GenerateStatus(ApplicationUserDto activeUser, string roles)
        {
            Status status = new Status();
            var acceptedRoles = roles.Split(',');
            foreach (var role in acceptedRoles)
            {
                if (activeUser.Roles.Contains(role))
                {
                    status.AccessStatus = true;
                    break;
                }
            }
            return status;
        }
    }
}