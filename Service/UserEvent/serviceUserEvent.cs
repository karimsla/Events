﻿

using Data.Infrastructure;
using Infrastructure;
using Model;
using MyFinance.Data.Infrastructure;

namespace Service
{
  public  class serviceUserEvent:servicePattern<UserEvent>,IServiceUserEvent
    {
        static IDatabaseFactory dbf = new DatabaseFactory();
        static IUnitOfWork uow = new UnitOfWork(dbf);
        public serviceUserEvent() : base(uow)
        {

        }

        public void participate(int idu, int ide)
        {
            UserEvent uev = new UserEvent();
            uev = this.Get(x => x.eventid == ide && x.userid == idu);
            if (uev!=null)
            {
                uev.participation = true;
                this.Update(uev);
                this.Commit();
            }
            else
            {
                UserEvent uevi = new UserEvent();
                uevi.eventid = ide;
                uevi.userid = idu;
                uevi.participation = true;
                this.Add(uevi);
                this.Commit();
            }
           
        }



        public void like(int idu, int ide)
        {
            UserEvent uev = new UserEvent();
            uev = this.Get(x => x.eventid == ide && x.userid == idu);
            if (uev != null)
            {
                if (uev.like == false) { uev.like = true; }
                
                if (uev.like == true) { uev.like = false; }
                   
                this.Update(uev);
                this.Commit();
            }
            else
            {
                UserEvent uevi = new UserEvent();
                uevi.eventid = ide;
                uevi.userid = idu;
                uevi.like = true;
                this.Add(uevi);
                this.Commit();
            }
        }


    }
}
