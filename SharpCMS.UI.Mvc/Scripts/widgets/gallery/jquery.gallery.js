(function($) {

    function Redman(elem) {
        this.init(elem);
    }

    Redman.prototype.init = function(elem) {
        this.element = $(elem).addClass("wu-tang").css("color", "red");
    };
    Redman.prototype.destroy = function() {
        this.element.removeClass("wu-tang").removeAttr("style");
    };

    $.fn.redman = function() {
        return this.each(function() {
            // Instantiate a new Redman, storing the instance in the element's data cache
            $.data(this, "redman", new Redman(this));
        });
    };
})(jQuery);

(function($) {
    // The jQuery.aj namespace will automatically be created if it doesn't exist
    $.widget("aj.filterable", {
    // These options will be used as defaults
        options: { className: "" },
        _create: function() {
            // The _create method is where you set up the widget
        },
        // Keep various pieces of logic in separate methods
        filter: function() {
            // Methods without an underscore are "public"
        },
        _hover: function() {
            // Methods with an underscore are "private"
        },
        _setOption: function(key, value) {
            // Use the _setOption method to respond to changes to options
            switch (key) {
            case "length":
                break;
            }
            $.Widget.prototype._setOption.apply(this, arguments);
        },
        destroy: function() {
            // Use the destroy method to reverse everything your plugin has applied
            $.Widget.prototype.destroy.call(this);
        }
    });
})(jQuery);