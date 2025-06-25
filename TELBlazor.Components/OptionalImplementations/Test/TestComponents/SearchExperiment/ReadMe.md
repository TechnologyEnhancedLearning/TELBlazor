# Search Component

The search component is given a search service using the search interface.
The interface requires a search endpoint and a search suggestion endpoint.

# Usage
For searching

# NoJs

The component has parameter for the search button action. On search it will hit the specified controller.
IT receives search information via the same interface so it can be populated in a view.

# Future
It currently doesnt use a cancellation token, and doesnt keep a list of suggestion responses.
If it kept the response from the first suggestion list and whittled them down from that list as long
as there are no deletions it would only need to call suggestions once. Assuming suggestions give everything.
if it will make a call per press it needs a cancel token potentially. 