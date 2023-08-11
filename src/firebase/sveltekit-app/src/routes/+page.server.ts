export function load({ cookies }) {
	const visited = cookies.get('visited');

	return {
		visited
	};
}