(function() {
	var a = window.jQuery;
	if (typeof a == 'undefined')
		return;
	a.fn.fileuploader = function(action, onSubmit, onCompleted) {
		var uploader = new qq.FileUploaderBasic({
			button: this[0],
			action: action,
			onSubmit: onSubmit,
			onComplete: onCompleted
		});
	};
})()