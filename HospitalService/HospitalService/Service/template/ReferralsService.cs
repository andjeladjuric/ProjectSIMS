using HospitalService.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Service.template
{
    public class ReferralsService : UpdateMedicalRecord
    {
        private Referral referral;

        public ReferralsService(Referral newReferral) {

            referral = newReferral;
        }
        public override void MakeChanges(MedicalRecord record)
        {
            record.Referrals.Add(referral);
        }
    }
}
