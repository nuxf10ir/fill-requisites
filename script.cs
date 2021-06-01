
function processTask($task, callback) {
	const $variants = $(".variant", $task);
	$variants.removeAttr('style');
	$('.field__quest', $task).html('<div class="placeholder"></div>');
	$task.fadeIn(function () {

		$(".variant", $task)
			.draggable({
				snap: ".placeholder",
				start: function (event, ui) {
					ui.helper.removeClass('red');
				}
			});
		$('.placeholder', $task).each(function( index, element )  {
			const $place = $(element)
			$place.droppable({
				drop: function( event, ui ) {
					const $dragged = ui.draggable;
					if ($dragged.is('.incorrect')) {
						$dragged.addClass('red');
						return false;
					}
					$dragged.fadeOut(function() {
						$place.droppable("destroy");
						$(this).draggable("destroy");
						$place.replaceWith($(this).clone().contents());
						if (!$variants.filter('.ui-draggable.correct').length) {
							callback();
						}
					});

				}
			});
		});
	});


}

$(function () {
	const $message1 = $("#message_1");
	const $message2 = $("#message_2");
	const $message3 = $("#message_3");
	const $message4 = $("#message_4");
	const $task1 = $("#task_1");
	const $task2 = $("#task_2");
	const $task3 = $("#task_3");

	$('button', $message1)
		.add('button', $message4)
		.on('click', function () {
			$message1.fadeOut(function () {
				processTask($task1, function () {
					$task1.fadeOut(function () {
						$message2.fadeIn();
					});
				});
			});
		});

	$('button', $message2).on('click', function () {
		$message2.fadeOut(function () {
			processTask($task2, function () {
				$task2.fadeOut(function () {
					$message3.fadeIn();
				});
			});
		});
	});

	$('button', $message3).on('click', function () {
		$message3.fadeOut(function () {
			processTask($task3, function () {
				$task3.fadeOut(function () {
					$message4.fadeIn();
				});
			});
		});
	});







/*
	$droppables
		.each(function( index, element )  {
			const $dropable = $(element)
			const $place = $dropable.find('.item__placeholder');
			const accept = $place
				.data('accept')
				.split(',')
				.map(id => '#' + id)
				.join(',');
			$place.droppable({
				accept,
				drop: function( event, ui ) {
					$dropable.removeClass('active');
					ui.draggable.fadeOut().draggable("destroy");
					if (!$variants.find('.ui-draggable').length) {
						$message3.fadeIn();
					}
				}
			});
		});
*/
	$message1.fadeIn();
});
