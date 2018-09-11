using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqualCreekBusinessLayer.entites;

namespace SqualCreekBusinessLayer.infacing
{
    public interface ICommon
    {
        void AddRoleToMember(Guid userID);
        Cart SaveOrder(Cart cart);

    }
}
