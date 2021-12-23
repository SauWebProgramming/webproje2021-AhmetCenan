$(function () {
    $('#list1').on('change', function () {
        var count = 1
        //console.log($('#list1'))
        var aratext = " x "
        var cloned = $('#list1').find(':selected')[0].innerHTML
        console.log($('#text1').val())
        console.log(cloned.includes("Kola"))
        var splitle = cloned.split(" - ")
        console.log(splitle)
        splitle.splice(1)
        console.log(splitle)


        var cloned2 = $('#list1').find(':selected').clone()
        $('#list2').append(cloned2)

    })
})

//$('#list1').each(function (element) {
        //    if ($(this).val() == cloned.val()) {
        //        $(this).remove()
        //        var split = this.innerHTML.split(" x ")
        //        split.splice(0, 1);
        //        var count2 = split[0]
        //        count = parseInt(count2) + 1
        //    }
        //})