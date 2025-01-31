using MainCode.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Repository
{
    public class PaymentsRepository
    {
        private static List<Payment> allPayments;

        public PaymentsRepository()
        {
            allPayments = ReadGetAllRows();
        }

        public virtual List<Payment> ReadGetAllRows()
        {
            allPayments = new List<Payment>();
            foreach (var pay in Payment.PaymentsDataSet)
            {
                allPayments.Add(new Payment()
                {
                    PaymentID = pay.PaymentID,
                    OrderID = pay.OrderID,
                    PaymentDate = pay.PaymentDate,
                    PaymentMethod = pay.PaymentMethod,
                    Amount = pay.Amount,
                    Status = pay.Status,
                });
            }
            return allPayments;
        }

        public List<Payment> ReadRowByDate(string date)
        {
            PaymentsRepository repository = new PaymentsRepository();
            List<Payment> allOfThePayments = repository.ReadGetAllRows();

            string defaultDateString = "10/08/2008";
            string format = "dd/MM/yyyy";
            CultureInfo ci = new CultureInfo("en-za");

            DateTime dateTime;
            if (!DateTime.TryParseExact(date, format, ci, System.Globalization.DateTimeStyles.None, out dateTime))
            {
                dateTime = DateTime.ParseExact(defaultDateString, format, ci);
            }
            IEnumerable<Payment> payment = allOfThePayments.Where(c => c.PaymentDate.Date == dateTime.Date);
            List<Payment> payments = new List<Payment>();
            if (payment != null)
            {
                payments = payment.ToList();
            }
            else
            {
                payments = null;
            }
            return payments;
        }

        public List<Payment> ReadRowByDate(string beginDate, string endDate)
        {
            PaymentsRepository repository = new PaymentsRepository();
            List<Payment> allOfThePayments = repository.ReadGetAllRows();

            string defaultDateString = "10/08/2008";
            string format = "dd/MM/yyyy";
            CultureInfo ci = new CultureInfo("en-za");

            DateTime beginDateTime;
            if (!DateTime.TryParseExact(beginDate, format, ci, System.Globalization.DateTimeStyles.None, out beginDateTime))
            {
                beginDateTime = DateTime.ParseExact(defaultDateString, format, ci);
            }

            DateTime endDateTime;
            if (!DateTime.TryParseExact(endDate, format, ci, System.Globalization.DateTimeStyles.None, out endDateTime))
            {
                endDateTime = DateTime.ParseExact(defaultDateString, format, ci);
            }
            IEnumerable<Payment> payment = allOfThePayments.Where(c => c.PaymentDate.Date >= beginDateTime.Date && c.PaymentDate.Date <= endDateTime.Date);
            List<Payment> payments = new List<Payment>();
            if (payment != null)
            {
                payments = payment.ToList();
            }
            else
            {
                payments = null;
            }
            return payments;
        }

        public List<Payment> ReadRowByPaymentMethod(string method)
        {
            PaymentsRepository repository = new PaymentsRepository();
            List<Payment> allOfThePayments = repository.ReadGetAllRows();
            IEnumerable<Payment> payment = allOfThePayments.Where(c => c.PaymentMethod == method);
            List<Payment> payments = new List<Payment>();
            if (payment != null)
            {
                payments = payment.ToList();
            }
            else
            {
                payments = null;
            }
            return payments;
        }

        public List<Payment> ReadRowByPaymentStatus(string status)
        {
            PaymentsRepository repository = new PaymentsRepository();
            List<Payment> allOfThePayments = repository.ReadGetAllRows();
            IEnumerable<Payment> payment = allOfThePayments.Where(c => c.Status == status);
            List<Payment> payments = new List<Payment>();
            if (payment != null)
            {
                payments = payment.ToList();
            }
            else
            {
                payments = null;
            }
            return payments;
        }
    }
}
