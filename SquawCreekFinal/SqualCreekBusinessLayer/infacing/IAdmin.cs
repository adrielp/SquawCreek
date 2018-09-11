using SqualCreekBusinessLayer.entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqualCreekBusinessLayer.infacing
{
    public interface IAdmin
    {
        MgmtDailyPortal AdminPortalDisplay();
        void ScheduleEvent(EventInfo evt);
        EventInfo EditEvent(Guid id);
        void UpdateEvent(EventInfo evt);
        void RemoveEvent(Guid id);
        void RemoveEventRequest(Guid id);
        EventInfo GetRequestInfo(Guid id);
        List<entites.ShopItem> GetListOfCurrentShopItems();
        entites.ShopItem FindShopItem(Guid id);
        void UpdateShopItem(entites.ShopItem item);
        void AddShopItem(entites.ShopItem item);
        void RemoveShopItem(Guid id);
        void CancelTeeTime(Guid id);
        void RemoveUser(Guid id);
    }
}
