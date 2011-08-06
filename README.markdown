# Aspirator 
Aspirator is an easy to use paginator, written for ASP.NET MVC.
It was inspired by [will_paginate] plugin from Ruby-on-Rails.

Quick install:

The easiest way to install is via nuget.

```c-sharp
install-package aspirator.mvc3
```

This will install the package and add the library reference.
Note that the [nuget package][nuget_package] references the ASP.NET MVC 3 assembly.

## Basic usage

Aspirator works with any collection that implements the [IQueryable&lt;T&gt;][iqueryable] interface.

Define your list view model that will be used in views.

``` java
public class RegionsList : PagedList<Region> {
	public RegionsList(IQueryable<Region> source, int index, int pageSize)
	    : base(source, index, pageSize) {
	}
}
```

Controller code

```
public ViewResult Index(int? page, int pageSize = 10) {
    return View(new RegionsList(db.Regions.OrderBy(x=>x.Id), page - 1 ?? 0, pageSize));
}
```

View code

```
@using KiP.Web.Mvc.Pagination
```

```
 @Html.Pager(Model)
```

The paging links will look as follows:

```
← 1 2 3 4 5 6 7 8 9 →
```

There are various customizations available, accessible via parameters to Pager extension method: 

* Action name (default - current)
* Controller name (default - current)
* Html atrributes object (default - null)
* Pager container (default - div)
* Previous label (default - &larr;)
* Next label (default - &rarr;)
* Inner window  (default - 4)
* Outer window (default - 1)
* Separator (default - " ")
* Window links (default - true)

This code is [MIT][mit] licenced:

Copyright (c) 2011 Valentin Vasilyev


[nuget_package]: http://nuget.org/List/Packages/Aspirator.MVC3
[iqueryable]: http://msdn.microsoft.com/en-us/library/system.linq.iqueryable.aspx
[mit]: http://www.opensource.org/licenses/mit-license.php
[will_paginate]: https://github.com/mislav/will_paginate