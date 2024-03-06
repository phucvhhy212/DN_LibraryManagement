using LibraryAPI.Helpers;
using LibraryCore.ViewModels;

namespace LibraryAPI.Services
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _configuration;
        public VnPayService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model)
        {
            
            var vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", _configuration["VnPay:vnp_Version"]);
            vnpay.AddRequestData("vnp_Command", _configuration["VnPay:vnp_Command"]);
            vnpay.AddRequestData("vnp_TmnCode", _configuration["VnPay:vnp_TmnCode"]);
            vnpay.AddRequestData("vnp_Amount", (model.Amount * 10000000).ToString()); //Số tiền thanh toán. Số tiền không 
            //mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND
            //(một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần(khử phần thập phân), sau đó gửi sang VNPAY
            //là: 10000000
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", _configuration["VnPay:vnp_CurrCode"]);
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
            vnpay.AddRequestData("vnp_Locale", _configuration["VnPay:vnp_Locale"]);

            vnpay.AddRequestData("vnp_OrderInfo", "Thanhtoandonhang");
            vnpay.AddRequestData("vnp_OrderType", "test"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", _configuration["VnPay:vnp_ReturnUrl"]);
            vnpay.AddRequestData("vnp_TxnRef", model.PaymentId); // Mã tham chiếu của giao dịch tại hệ 
                                                                          //thống của merchant.Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY.Không được
                                                                          //    trùng lặp trong ngày

            var paymentUrl = vnpay.CreateRequestUrl(_configuration["VnPay:vnp_Url"], _configuration["VnPay:vnp_HashSecret"]);

            return paymentUrl;
        }

        public VnPaymentResponseModel PaymentExecute(IQueryCollection collection)
        {
            var vnpay = new VnPayLibrary();
            foreach (var (key,value) in collection)
            {
                //get all querystring data
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, value.ToString());
                }
            }

            //Lay danh sach tham so tra ve tu VNPAY
            //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
            //vnp_TransactionNo: Ma GD tai he thong VNPAY
            //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
            //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

            string paymentId = vnpay.GetResponseData("vnp_TxnRef");
            long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
            long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
            string vnp_SecureHash = collection.First(x=>x.Key == "vnp_SecureHash").Value;
            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _configuration["VnPay:vnp_HashSecret"]);
            if (checkSignature)
            {
                return new VnPaymentResponseModel()
                {
                    Amount = vnp_Amount,
                    Success = true,
                    PaymentMethod = "VnPay",
                    PaymentId = paymentId,
                    TransactionId = vnpayTranId.ToString(),
                    Token = vnp_SecureHash,
                    VnPayResponseCode = vnp_ResponseCode,
                    VnPayTransactionStatus = vnp_TransactionStatus
                };
            }
            return new VnPaymentResponseModel()
            {
                Amount = vnp_Amount,
                VnPayResponseCode = vnp_ResponseCode,
                PaymentMethod = "VnPay",
                PaymentId = paymentId,
                TransactionId = vnpayTranId.ToString(),
                Token = vnp_SecureHash,
                VnPayTransactionStatus = vnp_TransactionStatus,
                Success = false
            };
        }
    }
}
