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
The [IQueryable&lt;T&gt;][iqueryable] is required to load only the displayed rows from the DB.


Define your list view model that will be used in views.

``` java
public class RegionsList : PagedList<Region> {
	public RegionsList(IQueryable<Region> source, int index, int pageSize)
	    : base(source, index, pageSize) {
	}
}
```

Controller code

``` java
public ViewResult Index(int? page, int pageSize = 10) {
    return View(new RegionsList(db.Regions.OrderBy(x=>x.Id), page - 1 ?? 0, pageSize));
}
```

View code

``` c-sharp
@using KiP.Web.Mvc.Pagination
```

``` razor
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

The defaults are sensible, so in most cases you only need to pass the collection to Pager method.

### Licence

This code is [MIT][mit] licenced:

Copyright (c) 2011 Valentin Vasilyev

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


[nuget_package]: http://nuget.org/List/Packages/Aspirator.MVC3
[iqueryable]: http://msdn.microsoft.com/en-us/library/system.linq.iqueryable.aspx
[mit]: http://www.opensource.org/licenses/mit-license.php
[will_paginate]: https://github.com/mislav/will_paginate