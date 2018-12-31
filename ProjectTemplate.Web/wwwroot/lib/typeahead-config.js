
$(function () {
    initTypeAhead();
})

function initTypeAhead() {

    $(".typeahead-auto").each(function (i) {

        var searchUrl = $(this).data("search-url");
        var resultTemplateSelector = $(this).data("result-template");

        var resultTemplate = Handlebars.compile($(resultTemplateSelector).html());

        var searchEngine = new Bloodhound({

            datumTokenizer: Bloodhound.tokenizers.whitespace,
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            remote: {
                url: searchUrl,
                wildcard: '%QUERY'
            }
        });

        $(this).on("change", function () {

            if ($(this).val() == '') {
                var idSelector = $(this).data("id");
                $(idSelector).val('');
            }
        })

        $(this).attr("dir", "rtl");
        $(this).typeahead('destroy');


        $(this).typeahead({
            minLength: 1,
            hint: false,
            classNames: {
                open: 'is-open',
                empty: 'is-empty',
                cursor: 'is-active',
                suggestion: 'typeahead-suggestion',
                selectable: 'typeahead-selectable',
                menu: "typeahead-menu",
                dataset: "typeahead-list-group"
            }
        }, {

                name: 'posList',
                displayKey: 'name',
                limit: 5,
                source: searchEngine.ttAdapter(),
                templates: {
                    notFound: '',
                    header: '',
                    suggestion: resultTemplate,
                    footer: ''
                },
            }).on('typeahead:asyncrequest', function () {
                $('.typeahead-spinner').show();
            }).on('typeahead:asynccancel typeahead:asyncreceive', function () {
                $('.typeahead-spinner').hide();
            }).on('typeahead:selected', function (e, datum) {

                
                var hiddenFieldId = $(this).data("id");
                $(hiddenFieldId).val(datum.id);

                $(this).closest('form').submit();

            }).on("typeahead:select", function (ev, suggestion) {

            });


    });
}
