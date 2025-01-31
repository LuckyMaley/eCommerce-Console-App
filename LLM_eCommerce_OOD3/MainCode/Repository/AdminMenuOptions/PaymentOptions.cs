using MainCode.Models;
using MainCode.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Repository.AdminMenuOptions
{
    
    public class PaymentOptions
    {
        public string FindPaymentsbyDate(PaymentsRepository paymentsRepository)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Console.WriteLine("Please enter payment date (dd/MM/yyyy): ");
            string inputDate = Console.ReadLine();
            if (inputDate != "")
            {
                List<Payment> payments = paymentsRepository.ReadRowByDate(inputDate);
                if (payments != null)
                {
                    payments.ForEach(b => stringBuilder.AppendLine($"ID: {b.PaymentID}, Payment Date: {b.PaymentDate.ToString("dd MMMM yyyy HH:mm")}, Payment Method: {b.PaymentMethod}, Amount: {b.Amount}, Status: {b.Status}"));
                }
                else
                {
                    stringBuilder.AppendLine("There is no payment in that date");
                }
            }
            else
            {
                stringBuilder.AppendLine("No Date entered");
            }
            return stringBuilder.ToString();
        }

        public string FindPaymentsbyBetweenDates(PaymentsRepository paymentsRepository)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Console.WriteLine("Please enter beginning order date (dd/MM/yyyy): ");
            string inputDate = Console.ReadLine();
            Console.WriteLine("Please enter ending order date (dd/MM/yyyy): ");
            string inputDateTwo = Console.ReadLine();
            if (inputDate != "" && inputDateTwo != "")
            {
                List<Payment> payments = paymentsRepository.ReadRowByDate(inputDate, inputDateTwo);
                if (payments != null)
                {
                    payments.ForEach(b => stringBuilder.AppendLine($"ID: {b.PaymentID}, Payment Date: {b.PaymentDate.ToString("dd MMMM yyyy HH:mm")}, Payment Method: {b.PaymentMethod}, Amount: {b.Amount}, Status: {b.Status}"));
                }
                else
                {
                    stringBuilder.AppendLine("There is no payment between those dates");
                }
            }
            else
            {
                stringBuilder.AppendLine("Please enter both beginning and end dates");
            }
            return stringBuilder.ToString();
        }

        public string FindPaymentsByMethod(PaymentsRepository paymentsRepository)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Console.WriteLine("Please enter the payment method: ");
            string input = Console.ReadLine();
            List<Payment> payments = paymentsRepository.ReadRowByPaymentMethod(input);
            if (payments.Count > 0)
            {
                payments.ForEach(b => stringBuilder.AppendLine($"ID: {b.PaymentID}, Payment Date: {b.PaymentDate.ToString("dd MMMM yyyy HH:mm")}, Payment Method: {b.PaymentMethod}, Amount: {b.Amount}, Status: {b.Status}"));
            }
            else
            {
                stringBuilder.AppendLine("There is no payment between those dates");
            }
            return stringBuilder.ToString();
        }

        public string FindPaymentsByStatus(PaymentsRepository paymentsRepository)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Console.WriteLine("Please enter the payment status: ");
            string input = Console.ReadLine();
            List<Payment> payments = paymentsRepository.ReadRowByPaymentStatus(input);
            if (payments.Count > 0)
            {
                payments.ForEach(b => stringBuilder.AppendLine($"ID: {b.PaymentID}, Payment Date: {b.PaymentDate.ToString("dd MMMM yyyy HH:mm")}, Payment Method: {b.PaymentMethod}, Amount: {b.Amount}, Status: {b.Status}"));
            }
            else
            {
                stringBuilder.AppendLine("There is no payment between those dates");
            }
            return stringBuilder.ToString();
        }
    }
}
