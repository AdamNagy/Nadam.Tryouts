declare interface HTMLElement {

	$nid(nid): HTMLElement;	

	$class(selector): HTMLElement[];

	$(selector): HTMLElement[];	

	/// <summary> 
	/// Iterates throught the direct childrend of the node, and flattens it into a list
	/// Optionally filters the list
	/// </summary>
	AllChildren(predicate: any): HTMLElement[];	

	ToParent(parent): HTMLElement;	
}