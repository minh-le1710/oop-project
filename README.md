# Các phương thức thanh toán - Minh hoạ OOP và OCP

## Giới thiệu

Đây là ứng dụng console C# minh hoạ 4 tính chất của lập trình hướng đối tượng (OOP) và nguyên tắc Open/Closed (chữ O trong SOLID) thông qua bài toán thanh toán đơn hàng.

Ứng dụng mô phỏng các cách thanh toán bằng mã QR, chuyển khoản, thẻ và Apple Pay. Đây chỉ là ví dụ học tập, không kết nối với ngân hàng hoặc cổng thanh toán thật.

## Yêu cầu

- .NET SDK 10.0

## Cách chạy

Tại thư mục của project, chạy:

```bash
dotnet build
dotnet run
```

`dotnet build` biên dịch và kiểm tra lỗi. `dotnet run` chạy toàn bộ project console.

## Cấu trúc project

| File | Vai trò |
|---|---|
| [`Order.cs`](Order.cs) | Lưu giá và trạng thái thanh toán của đơn hàng |
| [`IPaymentMethod.cs`](IPaymentMethod.cs) | Định nghĩa hợp đồng chung cho các cách thanh toán |
| [`PaymentMethodBase.cs`](PaymentMethodBase.cs) | Chứa quy trình thanh toán dùng chung |
| [`QRPayment.cs`](QRPayment.cs) | In chi tiết thanh toán bằng mã QR |
| [`TransferPayment.cs`](TransferPayment.cs) | In chi tiết thanh toán bằng chuyển khoản |
| [`CardPayment.cs`](CardPayment.cs) | In chi tiết thanh toán bằng thẻ và che số thẻ |
| [`ApplePayPayment.cs`](ApplePayPayment.cs) | Cách thanh toán được thêm mới để minh hoạ OCP |
| [`PaymentProcessorBeforeOcp.cs`](PaymentProcessorBeforeOcp.cs) | Ví dụ trước OCP, chọn cách thanh toán bằng `if/else` |
| [`Program.cs`](Program.cs) | Tạo các object và chạy chương trình minh hoạ |

## Bốn tính chất OOP

### 1. Encapsulation (đóng gói)

**Là gì:** Encapsulation gom dữ liệu và các method xử lý dữ liệu đó vào cùng một class, đồng thời kiểm soát quyền truy cập để dữ liệu không bị thay đổi tuỳ ý.

**Giải quyết vấn đề gì:** Nếu trạng thái thanh toán được phép gán trực tiếp từ bên ngoài, code có thể đánh dấu đơn đã thanh toán dù khách chưa trả tiền, hoặc đổi đơn đã thanh toán về chưa thanh toán để trả lần nữa.

**Minh hoạ trong code:** Trong [`Order.cs`](Order.cs), `Price` chỉ có `get` nên bên ngoài không thể đổi giá sau khi tạo đơn. `IsPaid` có `private set` nên bên ngoài chỉ đọc được. Muốn thay đổi trạng thái, code phải gọi `Pay()`. Method này trả về `false` nếu đơn đã được thanh toán.

**Kết quả chạy:** Lần gọi đầu tiên thanh toán thành công và đổi `IsPaid` thành `true`. Lần gọi thứ hai bị từ chối và chương trình in `Thanh toán thất bại`.

### 2. Abstraction (trừu tượng)

**Là gì:** Abstraction chỉ cho bên ngoài biết một object làm được gì và giấu cách thực hiện cụ thể bên trong. Trong C#, có thể dùng `interface` hoặc `abstract class` để tạo abstraction.

**Giải quyết vấn đề gì:** Code gọi thanh toán không cần biết cách in mã QR, số tài khoản hoặc số thẻ. Nó chỉ cần biết rằng mọi cách thanh toán đều có thể nhận một đơn hàng và trả về kết quả thành công hoặc thất bại.

**Minh hoạ trong code:** [`IPaymentMethod.cs`](IPaymentMethod.cs) định nghĩa hợp đồng `bool Pay(Order order)`. [`Program.cs`](Program.cs) làm việc với kiểu `IPaymentMethod`, không phụ thuộc vào chi tiết bên trong từng class thanh toán.

**Kết quả chạy:** `Program.cs` gọi cùng method `Pay(order)` cho tất cả các cách thanh toán mà không cần gọi method riêng cho QR, chuyển khoản, thẻ hoặc Apple Pay.

### 3. Inheritance (kế thừa)

**Là gì:** Inheritance cho phép class con nhận lại property và method của class cha, sau đó bổ sung hoặc viết lại phần riêng. Quan hệ kế thừa phải phù hợp với câu "class con là một class cha".

**Giải quyết vấn đề gì:** Nếu không kế thừa, mỗi class thanh toán phải tự viết lại các bước kiểm tra trạng thái, in số tiền, cập nhật đơn hàng và báo kết quả. Khi thay đổi một luật chung, lập trình viên phải sửa nhiều class và có thể bỏ sót một class.

**Minh hoạ trong code:** [`PaymentMethodBase.cs`](PaymentMethodBase.cs) là `abstract class` chứa method `Pay(Order order)` với quy trình dùng chung. `QRPayment`, `TransferPayment`, `CardPayment` và `ApplePayPayment` đều kế thừa class này. Mỗi class con chỉ cần `override PrintDetails()` để in chi tiết riêng.

`PaymentMethodBase` là `abstract` nên không thể tạo một "cách thanh toán chung chung" bằng `new PaymentMethodBase()`. Method `PrintDetails()` cũng là `abstract`, vì vậy class con bắt buộc phải viết cách hiển thị chi tiết của mình.

**Kết quả chạy:** Các class con chỉ viết phần chi tiết riêng nhưng vẫn cùng kiểm tra đơn đã thanh toán chưa, in số tiền và báo thành công hoặc thất bại nhờ code kế thừa từ class cha.

### 4. Polymorphism (đa hình)

**Là gì:** Polymorphism cho phép cùng một lời gọi method tạo ra hành vi khác nhau tuỳ theo kiểu thật của object tại thời điểm chạy.

**Giải quyết vấn đề gì:** Nếu không có đa hình, `Program.cs` phải dùng `if/else` hoặc `switch` để kiểm tra từng loại thanh toán. Mỗi khi thêm một loại mới, logic chọn loại phải được mở ra và sửa.

**Minh hoạ trong code:** [`Program.cs`](Program.cs) tạo một mảng `IPaymentMethod[]` chứa nhiều object khác nhau. Vòng `foreach` chỉ gọi một dòng `paymentMethod.Pay(order)`. Khi `Pay()` gọi `PrintDetails()`, C# chạy bản `override` theo kiểu thật của object:

- `QRPayment` in mã QR.
- `TransferPayment` in số tài khoản.
- `CardPayment` chỉ in bốn số cuối của thẻ.
- `ApplePayPayment` in tài khoản Apple Pay.

**Kết quả chạy:** Cùng một lời gọi `Pay(order)` nhưng terminal hiển thị bốn khối chi tiết thanh toán khác nhau.

## Open/Closed Principle (OCP)

Open/Closed Principle phát biểu rằng code nên mở để mở rộng chức năng nhưng đóng với việc sửa đổi logic cũ đang chạy đúng.

### Trước khi áp dụng OCP

[`PaymentProcessorBeforeOcp.cs`](PaymentProcessorBeforeOcp.cs) dùng chuỗi `if/else` để chọn giữa QR, chuyển khoản và thẻ. Khi truyền `ApplePay`, chương trình in:

```text
Không hỗ trợ cách thanh toán: ApplePay
```

Muốn hỗ trợ Apple Pay theo cách này, lập trình viên phải mở method `PrintPaymentDetails()` và thêm một nhánh `else if`. Việc sửa vào giữa logic cũ có thể ảnh hưởng những nhánh đã chạy đúng và yêu cầu kiểm tra lại toàn bộ method.

### Sau khi áp dụng OCP

Thiết kế sau OCP phụ thuộc vào `IPaymentMethod` và `PaymentMethodBase`. Để thêm Apple Pay, chỉ cần:

1. Thêm class `ApplePayPayment` kế thừa `PaymentMethodBase`.
2. `override PrintDetails()` để viết hành vi riêng.
3. Thêm một dòng `new ApplePayPayment()` vào danh sách trong `Program.cs`.

Các class thanh toán cũ, `Order`, `IPaymentMethod` và `PaymentMethodBase` không bị sửa.

Commit [`104030a`](https://github.com/minh-le1710/oop-project/commit/104030a69fd55e597acaac457e1d1cbf1e197a20) là bằng chứng: commit chỉ thêm file `ApplePayPayment.cs` và một dòng đăng ký trong `Program.cs`.

Việc thêm một dòng tại nơi khởi tạo danh sách là bước lắp cách thanh toán mới vào ứng dụng; logic xử lý và các class cũ vẫn giữ nguyên.

## Kết quả minh hoạ

Một phần output khi chạy chương trình:

```text
=== Trước OCP: dùng if/else ===
Không hỗ trợ cách thanh toán: ApplePay

=== Sau OCP: dùng đa hình ===
=== Thanh toán bằng QR ===
...
Thanh toán thành công

=== Thanh toán bằng Apple Pay ===
Tài khoản Apple Pay: demo@applepay.test
Số tiền: 200
Thanh toán thành công

=== Kiểm tra thanh toán ===
...
Thanh toán thành công
Thanh toán thất bại
```