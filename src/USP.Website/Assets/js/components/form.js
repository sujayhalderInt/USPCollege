window.usp = window.usp || {};
window.usp.form = window.usp.form || {};

window.usp.form = (function ($) {
    var model,
    removeErrorOnFocus = function () {
        
        $(model.inputLabel).on('click', function () {
            $(this).closest(model.inputWrapper).removeClass(model.isError);
        });
        
    },
    // checkRadioState = function (){
    //     $(document).on('click', 'input[type="submit"]', function (e) {
            
    //         var form = $(this).parents('form'),
    //         formId = $(form).attr('id'),
    //         radio = $(form).find('[type="radio"]'),
    //         radioChecked = $(form).find('[type="radio"]:checked'),
    //         radiobox = $(radio).closest('.form__check');

    //         console.log(radio.length > 0 && radioChecked == 0,radio.length, radioChecked.length, $(radiobox));

    //         if (radio.length > 0 && radioChecked.length == 0) {
    //             $(radiobox).addClass('form__check--error');
    //             // e.preventDefault();
    //             console.log(radiobox, form);
    //         } else {
    //             $(radiobox).removeClass('form__check--error');
    //             $(formId).submit();
    //         }

    //     });
    // },
    jqueryValidateOptions = function () {
        
        $.validator.addMethod("date",
        function(value, element) {
            return value.match(/^((0[1-9]|[12]\d|3[01])\/(0[1-9]|1[0-2])\/[12]\d{3})$/m) || value == '';
        },
        "Please enter a date in the format dd/mm/yyyy.");

        // $.validator.addMethod("radio",
        // function (value, element) {
        //     return false;
        // },
        // "Please enter a date in the format dd-mm-yyyy.");

        // $('.radio-select > input[type="radio"]').each(function (index, validationName) {
        //     $.validator.addMethod(validationName)
        // });


        jQuery.validator.setDefaults({
           
            highlight: function(element, errorClass, validClass) {   
              $(element).parents(".form__check").addClass('form__check--error').removeClass('form__check--success');
            },
            unhighlight: function(element, errorClass, validClass) {
              $(element).parents(".form__check").removeClass('form__check--error').addClass('form__check--success');
            },
            onfocusout: function(element) {
                 $(element).valid();
            },
        });

        $(['requiredif', 'regularexpressionif', 'rangeif']).each(function (index, validationName) {
            $.validator.addMethod(validationName,
                    function (value, element, parameters) {
                        // Get the name prefix for the target control, depending on the validated control name
                        var prefix = "";
                        var lastDot = element.name.lastIndexOf('.');
                        if (lastDot != -1) {
                            prefix = element.name.substring(0, lastDot + 1).replace('.', '_');
                        }
                        var id = '#' + prefix + parameters['dependentproperty'];
                        // get the target value
                        var targetvalue = parameters['targetvalue'];
                        targetvalue = (targetvalue == null ? '' : targetvalue).toString();
                        // get the actual value of the target control
                        var control = $(id);
                        if (control.length == 0 && prefix.length > 0) {
                            // Target control not found, try without the prefix
                            control = $('#' + parameters['dependentproperty']);
                        }
                        var controltype = control.attr('type');
                        if (controltype == 'radio') {
                            control = $(id + ":checked");
                        }
                        if (control.length > 0) {
                            var actualvalue = "";
                            switch (controltype) {
                                case 'checkbox':
                                    actualvalue = control.attr('checked').toString(); break;
                                case 'select':
                                    actualvalue = $('option:selected', control).text(); break;
                                default:
                                    actualvalue = control.val(); break;
                            }
                            // if the condition is true, reuse the existing validator functionality
                            if (targetvalue.toLowerCase() === actualvalue.toLowerCase()) {
                                var rule = parameters['rule'];
                                var ruleparam = parameters['ruleparam'];
                                return $.validator.methods[rule].call(this, value, element, ruleparam);
                            }
                        }
                        return true;
                    }
                );

        
            $.validator.unobtrusive.adapters.add(validationName, ['dependentproperty', 'targetvalue', 'rule', 'ruleparam'], function (options) {
                var rp = options.params['ruleparam'];
                options.rules[validationName] = {
                    dependentproperty: options.params['dependentproperty'],
                    targetvalue: options.params['targetvalue'],
                    rule: options.params['rule']
                };
                if (rp) {
                    options.rules[validationName].ruleparam = rp.charAt(0) == '[' ? eval(rp) : rp;
                }
                options.messages[validationName] = options.message;
            });
        });
       
    },
    addQualifications = function () {
        $(model.addQualifications).on('click', function (e) {
            e.preventDefault();
            var qual = $(model.wrapperQualification).first().clone(),
            upperB = $(model.wrapperQualification).length;
            $(qual).insertAfter($(model.wrapperQualification).last() );

            var qualification = $(model.wrapperQualification).last();

            var labelForSubject = 'PostBackModel_Qualifications_'+ upperB +'__Subject',
            nameSubject = 'PostBackModel.Qualifications['+ upperB +'].Subject',
            labelForType = 'PostBackModel_Qualifications_'+ upperB +'__Type',
            nameType = 'PostBackModel.Qualifications[' + upperB +'].Type',
            labelForGrade = 'PostBackModel_Qualifications_' + upperB +'__GradeOrPredictedGrade',
            nameGrade = 'PostBackModel.Qualifications[' + upperB +'].GradeOrPredictedGrade',
            labelForYear = 'PostBackModel_Qualifications_' + upperB +'__Year',
            nameYear = 'PostBackModel.Qualifications[' + upperB +'].Year';

            qualification.find('input').val('');
            qualification.find('.form__check').removeClass('form__check--success');
            qualification.find('label[for="PostBackModel_Qualifications_0__Subject"]').attr('for', labelForSubject);
            qualification.find('input[id="PostBackModel_Qualifications_0__Subject"]').attr('id', labelForSubject).attr('name', nameSubject);
            
            qualification.find('label[for="PostBackModel_Qualifications_0__Type"]').attr('for', labelForType);
            qualification.find('input[id="PostBackModel_Qualifications_0__Type"]').attr('id', labelForType).attr('name', nameType);

            qualification.find('label[for="PostBackModel_Qualifications_0__GradeOrPredictedGrade"]').attr('for', labelForGrade);
            qualification.find('input[id="PostBackModel_Qualifications_0__GradeOrPredictedGrade"]').attr('id', labelForGrade).attr('name', nameGrade);

            qualification.find('label[for="PostBackModel_Qualifications_0__Year"]').attr('for', labelForYear);
            qualification.find('input[id="PostBackModel_Qualifications_0__Year"]').attr('id', labelForYear).attr('name', nameYear);

        });
    },

        /**
         * Init class to initiate script
         * @public
         * @param {object} args is from doc-ready init file
         */
        init = function (args) {
            args = args || {};
            model = {
                init: function () {
                    this.inputWrapper = args.inputWrapper || '.form__check';
                    this.inputLabel = args.inputLabel || '.form__check input';
                    this.inputEmail = args.inputEmail || 'input[type="email"]'
                    this.isError = args.isError || 'form__check--error';

                    this.wrapperQualification = args.wrapperQualification || '.qualifications';
                    this.cloneQualification = args.cloneQualification || '.qualifications:first-of-type';
                    this.lastQualification = args.lastQualification || '.qualifications:last-of-type';
                    this.addQualifications = args.addQualifications || '.add-qualification';
                }
            };
            model.init();

            $.getScript("https://ajax.aspnetcdn.com/ajax/jquery.validate/1.17.0/jquery.validate.min.js")
            .done(function( script, textStatus ) {
                $.getScript("https://cdnjs.cloudflare.com/ajax/libs/jquery-validation-unobtrusive/3.2.9/jquery.validate.unobtrusive.min.js").done( function () {
                    jqueryValidateOptions();
                    // checkRadioState();
                });
            });

            $.getScript(baseUrl + "/assets/js/vendors/moment.min.js")
            .done(function( script, textStatus ) {
                $.getScript(baseUrl + "/assets/js/vendors/pikaday.js").done( function () {
                    $.getScript(baseUrl + "/assets/js/vendors/pikaday.jquery.js").done( function () {

                    $('.date').pikaday({ 
                        format: 'DD/MM/YYYY',
                        yearRange: [1900, 2040]
                    });
                });
                });
            });
            
            removeErrorOnFocus();
            if ($(model.addQualifications).length > 0) {
                addQualifications();
            }
            
        };

    /**
     * Returns init
     * @return public methods/functions
     * @public
     */
    return {
        init: init
    };

}(jQuery));