﻿using Data.Infrastructure;
using Infrastructure;
using Model;
using MyFinance.Data.Infrastructure;


namespace Service
{ 
   public class serviceUniv:servicePattern<University>,IserviceUniv
    {
        static IDatabaseFactory dbf = new DatabaseFactory();
        static IUnitOfWork uow = new UnitOfWork(dbf);
        public serviceUniv() : base(uow)
        {

        }
    }
}
