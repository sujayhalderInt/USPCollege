window.usp = window.usp || {};
window.usp.calculator = window.usp.calculator || {};

window.usp.calculator = (function ($) {
    var model,
    changeSystems = function() {
        $(model.pointSystem).on('change', function() {
            var system =  $(this).find(':selected').val();
            $(model.points).find('span').addClass('hidden');
            $(model.points).find('span.' + system).removeClass('hidden');
            $(model.points).find('input').val('');
        });
    },
    calculateGrade = function() {
        $(model.grades).on('change', function() {
            var calculatedGrade = 0, subjects = 0, system = $(model.pointSystem).find(':selected').val(), $this = $(this);

            if( !($this.val().match(/^[0-9]{1,2}$/)) ) { 
                $this.val('');
                return
            };

            $(model.grades).each(function(index, content){
                var multiplier = $(content).data('multiplier-' + system),
                grade = $(content).val();

                if (grade != '') {
                    subjects += parseInt(grade);
                    calculatedGrade += multiplier * parseInt(grade);
                }
            });

            $(model.showCalculated).html( Math.round((calculatedGrade / subjects) * 100) / 100 );

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
                    this.calculator = args.calculator || '.points';
                    this.pointSystem = args.pointSystem || '.points__system select';
                    this.points = args.points || '.points__grades';
                    this.grades = args.grades || model.points + ' input';
                    this.showCalculated = args.showCalculated || '.grade-calc';
                }
            };
            model.init();

            changeSystems();
            calculateGrade();
            
           
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