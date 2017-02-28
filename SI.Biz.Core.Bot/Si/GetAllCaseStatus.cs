using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Si
{
    public enum CaseStatusOptions
    {
        R=4,
        IP=5,
        CL=6,
        NF=7,
        CA=8,
        CE=9,
        CC=17
    };
    [Serializable]
    public class GetAllCaseStatus
    {
        public CaseStatusOptions status{get;set;}
        public static IForm<GetAllCaseStatus> BuildForm()
        {
            return new FormBuilder<GetAllCaseStatus>()
                        .Message("Choose a Status")
                        .Build();
        }

    }
}