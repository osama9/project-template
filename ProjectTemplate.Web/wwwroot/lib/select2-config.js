var AppSelect2 = (function () {
    var $el;
    var searchUrl;
    var placeholderText = '-- Select --';
    var disablePaging = false;

    var defaultOptions = {
        width: null,
        allowClear: true,
        minimumInputLength: 0,
        dir: 'rtl',
        language: 'ar'

    };

    var init = function init(el, options, _disablePaging) {
        $.fn.select2.defaults.set("theme", "bootstrap");

        var options = options || {};

        disablePaging = _disablePaging || false;
        $el = $(el);

        searchUrl = $el.attr('data-search-url');
        placeholderText = $el.data('placeholder');

        var resultTemplateSelector = $el.data('template-id');

        if (resultTemplateSelector) {

            var renderedTemplate = Handlebars.compile($(resultTemplateSelector).html());

            options.templateResult = function (state) {
                if (state.loading) return state.id;

                var markupHtml = renderedTemplate(state);

                return $(markupHtml);
            };
        }


        options.placeholder = placeholderText;
        search(options);


    }

    var search = function (customOptions) {
        var options = buildOptions(customOptions);

        return $el.select2(options).on("change", function (e) {
            $(this).valid(); //jquery validation script validate on change
        }).on("select2:open", function () { //correct validation classes (has=*)
            if ($(this).parents("[class*='has-']").length) { //copies the classes
                var classNames = $(this).parents("[class*='has-']")[0].className.split(/\s+/);

                for (var i = 0; i < classNames.length; ++i) {
                    if (classNames[i].match("has-")) {
                        $("body > .select2-container").addClass(classNames[i]);
                    }
                }
            } else { //removes any existing classes
                $("body > .select2-container").removeClass(function (index, css) {
                    return (css.match(/(^|\s)has-\S+/g) || []).join(' ');
                });
            }
        });
    }

    var buildOptions = function (customOptions) {
        var ajax;

        if (searchUrl)
            ajax = buildAjaxOptions();

        var options = $.extend({}, defaultOptions, customOptions, ajax);

        return options;
    };

    var buildAjaxOptions = function () {

        return {
            ajax: {
                url: searchUrl,
                dataType: 'json',
                delay: 250,
                data: function (params) {
                    return {
                        keyword: params.term,
                        pageNumber: params.page || 1,
                    };
                },
                processResults: function (data, params) {
                    var more = false;
                    params.page = params.page || 1;

                    if (!disablePaging) {
                        more = (params.page * 20) < data.totalResultsCount;
                        params.page = data.pageNumber || 1;

                    }



                    return {
                        results: data.results,
                        pagination: {
                            more: more
                        }
                    };
                }
            }
        }
    };


    return {
        init: init
    };
})();