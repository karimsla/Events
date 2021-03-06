﻿using Model;
using System;
using System.Collections.Generic;


namespace Service.EventFolder
{
   public  interface IserviceEvent:IservicePattern<Event>
    {

        void edit_event(Event _event);
       // void delete_event(Event _event);
        void create_event(Event _event);
        List<Event> search_Event(string keyword);
        List<Event> search_event_date(DateTime date);
        List<Event> search_event_university(int univid);
        List<Event> search_event_theme(int theme);
        void acceptEvent(int eventid,int idadmin);
        void refuseEvent(int eventid);
        dynamic Eventstat();


    }

}
