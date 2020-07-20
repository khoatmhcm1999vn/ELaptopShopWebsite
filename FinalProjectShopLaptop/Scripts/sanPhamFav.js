
$(document).ready(function ()
{
    //nút yêu thích
    $('#favorite').click(() =>
    {
        $.ajax({
            type: "post",
            url: "/SanPham/FavouriteSanPham",
            data:
            {
                sanPham: currentSanPham
            },
            success: function (response)
            {
                switch (response.result)
                {
                    case 1:
                        {
                            $('#annouce').text(response.message);
                            $('.addSanPhamStatus').css({ 'display': 'block' });
                            $('.addSanPhamStatus').fadeOut(1500);
                            $('#imgLike').attr('src', response.imgsrc);
                            break;
                        }
                    default:
                        {
                            $('#annouce').text(response.message);
                            $('.addSanPhamStatus').css({ 'display': 'block' });
                            $('.addSanPhamStatus').fadeOut(1500);
                            break;
                        }
                }
            }
        });
    })
});
