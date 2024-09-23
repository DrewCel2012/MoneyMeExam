$(function () {

    $('#AmountRequired').on('input', function () {
        debugger;
        var result = $('#AmountRequired').val();
        $('div.amountvalue').text('$ ' + result.toString());
    });

    $('#Term').on('change', function () {
        debugger;
        var result = $('#Term').val();

        if ($('option:selected').val() == 2 && result < 6) {
            result = 6;
            $('#Term').val(result);

            $.alert({
                title: 'Error',
                content: 'Term should be greater than or equal to <b>({0})</b> months if <b>ProductB</b> is selected!'.format(result),
                type: 'red',
                typeAnimated: true,
                buttons: {
                    close: {
                        text: 'OK',
                        btnClass: 'btn-red'
                    }
                }
            });
        }
        
        $('div.termvalue').text(result.toString() + ' Month(s)');
    });

    $("#ProductId").on("change", function () {
        debugger;
        if ($('option:selected').val() == 2) {
            $('#Term').val(6);
            $('div.termvalue').text('6 Month(s)');
        }
    });

});