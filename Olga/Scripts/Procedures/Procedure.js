function getProductInfo(link, productId) {
    
    console.log("productId = " + productId);

    $.ajax({
        type: "POST",
        url: link,
        data: { productId: productId },
        cache: false,
        beforeSend: function () { },
        success: function (request) {
            if (request.success === true) {

                console.log("request = " + request.responseText);
                var product = JSON.parse(request.responseText);

                $("#CountryName").text(product.countryName);
                $("#PharmaceuticalForm").text(product.pharmaceuticalForm);
                $("#Strength").text(product.strength);
                $("#MarketingAuthorizNumber").text(product.marketingAuthorizNumber);
                $("#ProductCode").text(product.productCode);
                $('.productInfo img').hide();
                $('#CountryFlag').attr('src', "../../Content/images/countries/" + product.flagSrc);

                var productLink = jQuery('<a>').attr('href', '/Product/ShowDocuments/' + productId + '?countryId=' + product.countryId).text(product.productName);
                jQuery('#ProductNameWithLink').append(productLink);

            } else {
                console.log("request = " + request.responseText);
                toastr.error(request.responseText);
            }
        }
    });
}