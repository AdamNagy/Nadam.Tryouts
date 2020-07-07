/*
usage:
ndm("default:div.container") returns <div class="container"></div>
	. -> class
	# -> id
	* -> nid
	(space) -> child
	[attribute name = attrbiute value] -> attribute
	{key} -> adds second patameters (config) 'key' named property as object to ctor as parameter
ndm(custom:parallax#my-parallax.some-class, {...}) returns <ndm-parallax class="some-class" id="my-parallax">...</ndm-parallax>
ndm("<div><p></p><img><img><section></section></div>...") returns <div><p></p><img><img><section></section></div>
ndm("template:some-template-id") returns instantiated html document
*/

function ndm(param, config) {

}