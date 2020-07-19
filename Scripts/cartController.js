
var cart =
{
    init: function ()
    {
        cart.regEvents();
    },
    regEvents: function ()
    {
        $('#btnContinue').off('click').on('click', function ()
        {
            window.location.href = "/";
        });

        $('#btnPayment').off('click').on('click', function ()
        {
            window.location.href = "/Cart/Payment";
        });

        //Them
        $("#soLuongGioHang").keyup(function ()
        {
            var SoLuongConLai = $("#soLuongKho").val();
            var Tong = $("#tongHoaDon").html();
            var Gia = $("#giaHoaDon").html();
            var SoLuong = $("#soLuongGioHang").val();
            if (SoLuongConLai - SoLuong < 0)
            {
                alert("Số lượng sản phẩm còn trong kho không đủ đáp ứng");
                //alert(Tong, Gia);
            }
            else if (SoLuong < 0) { alert("Số lượng sản phẩm không thể nhỏ hơn 0"); }
            else
            {
                //Tong = Tong - Gia * SoLuongCu;
                var e2 = Gia.replace(/,(?=\d{3})/g, '');
                var d = parseInt(SoLuongConLai);
                var e = c + d;

                var c = parseInt(SoLuong);
                //var c1 = parseInt(Tong);
                var d1 = parseInt(e2, 10);
                var e1 = c * d1;

                //SoLuong = SoLuong + SoLuongConLai;
                //var b = parseInt(SoLuong);
                /*
                alert(e1);
                alert(c);
                alert(Tong);
                alert(Tong);
                alert(Gia);
                */

                var f = c.toString();
                //$(this).attr("type", SoLuong);
                $("#soLuongGioHang").html(f);
                $("#hoadon").html(Tong);
                //$("#hoadon").text(Tong);
            }
        });

        $('#btnUpdate').off('click').on('click', function ()
        {
            var listProduct = $('.txtQuantity');
            var cartList = [];
            $.each(listProduct, function (i, item)
            {
                cartList.push({
                    Quantity: $(item).val(),
                    SanPham: {
                        Id: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res)
                {
                    if (res.status == true)
                    {
                        window.location.href = "/Cart";
                    }
                }
            })
        });

        $('#btnDeleteAll').off('click').on('click', function ()
        {
            $.ajax({
                url: '/Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res)
                {
                    if (res.status == true)
                    {
                        window.location.href = "/Cart";
                    }
                }
            })
        });

        $('.btn-delete').off('click').on('click', function (e)
        {
            e.preventDefault();
            $.ajax({
                data: { id: $(this).data('id') },
                url: '/Cart/Delete',
                dataType: 'json',
                type: 'POST',
                success: function (res)
                {
                    if (res.status == true)
                    {
                        window.location.href = "/Cart";
                    }
                }
            })
        });
    }
}
cart.init();
