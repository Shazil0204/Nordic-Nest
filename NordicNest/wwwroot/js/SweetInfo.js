function showInfo(infoType) {
	let title, text;

	switch (infoType) {
		case 'CurrentAmount':
			title = 'Current Amount';
			text = 'This is the available amount you have in your bank account';
			break;
		case 'UsableAmount':
			title = 'Usable Amount';
			text = 'This is the amount that you can use without having any trouble with your saving loan and subscriptions and your reserve amount';
			break;
		case 'Transaction':
			title = 'Transactions';
			text = 'Please click on the 3 dots on the top left of this div to see all the transactions of the last 30 days';
			break;
		case 'MonthlyChart':
			title = 'Monthly Chart';
			text = 'This is your monthly money usage chart';
			break;
		case 'UserReserver':
			title = 'User Reserver';
			text = 'This is the amount you have reserved. You can modify it by clicking on the edit button on the top left side of this div';
			break;
		case 'SystemReserver':
			title = 'System Reserver';
			text = 'This is the amount the system has reserved';
			break;
		case 'Savings':
			title = 'Savings';
			text = 'Please click on the 3 dots on the top left of this div to see all the saving accounts';
			break;
		case 'Subscriptions':
			title = 'Subscriptions';
			text = 'Please click on the 3 dots on the top left of this div to see all the subscriptions in the next 30 days';
			break;
		case 'Loans':
			title = 'Loans';
			text = 'Please click on the 3 dots on the top left of this div to see all the loans';
			break;
		default:
			title = 'Default Title';
			text = 'Default Text';
			break;
	}

	Swal.fire({
		title: title,
		text: text,
		icon: 'info',
		confirmButtonText: 'OK'
	});
}