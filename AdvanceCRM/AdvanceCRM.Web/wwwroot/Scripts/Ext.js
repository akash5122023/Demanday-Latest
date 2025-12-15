
var Serenity; (function (e) {
    var t; (function (t) {
        var r = function () {
            function t(e) {
                this.inner = e; this.setters = Object.create(null); this.refs = Object.create(null); this.set = this.set.bind(this); this.setter = this.setter.bind(this)
            } t.prototype.get = function (e) {
                return this.refs[e]
            }; t.prototype.set = function (e, t) {
                this.refs[e] = t; if (this.inner != null) this.inner(e, t)
            }; t.prototype.setter = function (e) {
                var t = this.setters[e]; if (t != null) return t; var r = this.set; t = function (t) {
                    r(e, t)
                }; this.setters[e] = t; return t
            }; t.prototype.loadFrom = function (t, r) {
                r = Q.coalesce(r, Object.keys(this.refs)); for (var n = 0, i = r; n < i.length; n++) {
                    var o = i[n]; var a = this.refs[o]; if (a == null) continue; if (a.isWidgetWrapper) {
                        a = a.widget; if (a) e.EditorUtils.loadValue(a, { name: o }, t)
                    } else if (a.nodeType) {
                        t[o] = a.value
                    } else if (a.element) {
                        e.EditorUtils.loadValue(a, { name: o }, t)
                    }
                }
            }; t.prototype.saveTo = function (t, r, n) {
                r = Q.coalesce(r, Object.keys(this.refs)); for (var i = 0, o = r; i < o.length; i++) {
                    var a = o[i]; var s = this.refs[a]; if (s.isWidgetWrapper) {
                        if (n || !s.props.oneWay) {
                            s = s.widget; if (s) e.EditorUtils.saveValue(s, { name: a }, t)
                        }
                    } else if (s.nodeType) {
                        if (n || !s.dataset || !s.dataset.oneWay) t[a] = s.value
                    } else if (s.element) {
                        if (n || !s.options || !s.options.oneWay) {
                            e.EditorUtils.saveValue(s, { name: a }, t)
                        }
                    }
                }
            }; return t
        }(); t.EditorRefs = r
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));

var Serenity; (function (e) {
    var t; (function (e) {
        var t = function (e) {
            __extends(t, e); function t() {
                return e !== null && e.apply(this, arguments) || this
            } t.prototype.render = function () {
                var e = this.props; var t = typeof e.width == "string" || e.width == null ? null : typeof e.width == "number" ? e.width + "px" : e.width; var r = t == null ? null : t == "0" ? { display: "none" } : { width: t }; return React.createElement("label", { className: "caption", htmlFor: e.htmlFor, title: e.hint, style: r }, e.required && React.createElement("sup", { title: Q.text("Controls.PropertyGrid.RequiredHint") }, "*"), e.children)
            }; return t
        }(React.Component); e.Label = t
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));

var Serenity; (function (e) {
    var t; (function (e) {
        var t = function (e) {
            __extends(t, e); function t() {
                return e !== null && e.apply(this, arguments) || this
            } t.prototype.render = function () {
                return React.createElement("div", { className: "vx" })
            }; t.prototype.shouldComponentUpdate = function () {
                return false
            }; return t
        }(React.Component); e.ValidationMark = t
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));

var Serenity; (function (e) {
    var t; (function (e) {
        var t = function (t) {
            __extends(r, t); function r() {
                return t !== null && t.apply(this, arguments) || this
            } r.prototype.componentWillReceiveProps = function (e, t) {
                if (e != null && e.setRef !== this.props.setRef) {
                    this.editorRef = null
                }
            }; r.prototype.render = function () {
                var t = this.props; var r; if (t.label != null) r = t.label(this.props); else if (t.caption !== false) {
                    r = React.createElement(e.Label, { htmlFor: t.htmlFor === undefined ? t.id : t.htmlFor, hint: t.hint, width: t.labelWidth, required: t.required }, t.caption)
                } var n = "field"; if (t.name) {
                    n += " " + t.name
                } if (t.className) {
                    n += " " + t.className
                } var i = t.name; if (this.props.setRef != null && this.editorRef == null) {
                    var o = this.props.setRef; this.editorRef = function (e) {
                        o(i, e)
                    }
                } var a = { className: "editor", name: i, id: this.props.id, required: this.props.required, ref: this.editorRef }; return React.createElement("div", { className: n }, r, t.editor != null && t.editor(a), t.children, t.vx !== false && React.createElement(e.ValidationMark, null), React.createElement("div", { className: "clear" }))
            }; return r
        }(React.Component); e.Field = t
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));

var Serenity; (function (e) {
    var t; (function (t) {
        var r = function (r) {
            __extends(n, r); function n() {
                var e = r !== null && r.apply(this, arguments) || this; e.text = Q.prefixedText(e.props.localTextPrefix); return e
            } n.prototype.getCaption = function () {
                return this.text(this.props.title, this.props.name)
            }; n.prototype.getHint = function () {
                var e = this.text(this.props.hint, this.props.name + "_Hint"); if (e == null) return this.getCaption(); return e || null
            }; n.prototype.getPlaceHolder = function () {
                return this.text(this.props.placeholder, this.props.name + "_Placeholder")
            }; n.prototype.getClassName = function () {
                var e = this.props.cssClass || ""; if (!Q.isEmptyOrNull(this.props.formCssClass)) {
                    e += (e ? " " : "") + this.props.formCssClass
                } return e
            }; n.prototype.getHtmlFor = function (t) {
                var r; if ((t === e.RadioButtonEditor || t === e.BooleanEditor) && (this.props.editorParams == null || !!!this.props.editorParams["labelFor"])) {
                    r = null
                } return r
            }; n.prototype.getEditorType = function () {
                if (this.props.editorType == null) return e.StringEditor; if (typeof this.props.editorType == "string") return e.EditorTypeRegistry.get(this.props.editorType); return this.props.editorType
            }; n.prototype.getEditorId = function () {
                return (this.props.idPrefix || "") + (this.props.name || "")
            }; n.prototype.getMaxLength = function () {
                return this.props.maxLength > 0 ? this.props.maxLength : null
            }; n.prototype.render = function () {
                var e = this; var r = this.getEditorType(); return React.createElement(t.Field, {
                    className: this.getClassName(), caption: this.getCaption(), id: this.getEditorId(), name: this.props.name, labelWidth: this.props.labelWidth, htmlFor: this.getHtmlFor(r), hint: this.getHint(), required: this.props.required, setRef: this.props.setRef, editor: function (t) {
                        return React.createElement(r, __assign({}, t, { maxlength: e.getMaxLength() }, e.props.editorParams, { setOptions: e.props.editorParams }))
                    }
                }, this.props.children)
            }; return n
        }(React.Component); t.PropertyField = r
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));
var Serenity; (function (e) {
    var t; (function (e) {
        var t = function (e) {
            __extends(t, e); function t() {
                return e !== null && e.apply(this, arguments) || this
            } t.prototype.render = function () {
                return React.createElement("div", { className: "category-title", onClick: this.props.onClick }, React.createElement("a", { className: "category-anchor", id: this.props.categoryId }, this.props.children), this.props.collapsed != null && (this.props.collapsed ? t.collapsedIcon : t.expandedIcon))
            }; t.collapsedIcon = React.createElement("i", { className: "fa fa-plus" }); t.expandedIcon = React.createElement("i", { className: "fa fa-minus" }); return t
        }(React.Component); e.CategoryTitle = t
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));
var Serenity; (function (e) {
    var t; (function (e) {
        var t = function (e) {
            __extends(t, e); function t() {
                return e !== null && e.apply(this, arguments) || this
            } t.prototype.getBreakClass = function () {
                var e = "line-break"; var t = this.props.breakClass.split(" "); if (t.indexOf("line-break-xs") >= 0) {
                } else if (t.indexOf("line-break-sm") >= 0) {
                    e += " hidden-xs"
                } else if (t.indexOf("line-break-md") >= 0) {
                    e += " hidden-sm"
                } else if (t.indexOf("line-break-lg") >= 0) {
                    e += " hidden-md"
                } return e
            }; t.prototype.render = function () {
                return React.createElement("div", { className: this.getBreakClass(), style: { width: "100%" } })
            }; return t
        }(React.Component); e.CategoryLineBreak = t
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));
var Serenity; (function (e) {
    var t; (function (e) {
        var t = function (e) {
            __extends(t, e); function t() {
                return e !== null && e.apply(this, arguments) || this
            } t.prototype.handleClick = function (e) {
                e.preventDefault(); this.props.onClick && this.props.onClick(e); var t = $("a[id=" + this.props.categoryId + "]"); var r = t.closest(".category"); if (r.hasClass("collapsed")) r.children(".category-title").click(); if (!t && !t.fadeTo) return; var n = function () {
                    t.fadeTo(100, .5, function () {
                        t.fadeTo(100, 1, function () {
                        })
                    })
                }; if (r.closest(":scrollable(both)").length === 0) n(); else { var i = r.scrollintoview; i && i.scrollintoview({ duration: "fast", direction: "y", complete: n }) }
            }; t.prototype.getLink = function () {
                if (Q.isEmptyOrNull(this.props.categoryId)) return null; return "#" + this.props.categoryId
            }; t.prototype.render = function () {
                var e = this; return React.createElement("a", {
                    className: "category-link", tabIndex: -1, onClick: function (t) {
                        return e.handleClick(t)
                    }, href: this.getLink()
                }, this.props.children)
            }; return t
        }(React.Component); e.CategoryLink = t
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));
var Serenity; (function (e) {
    var t; (function (e) {
        var t = function (t) {
            __extends(r, t); function r() {
                var e = t !== null && t.apply(this, arguments) || this; e.text = Q.prefixedText(e.props.localTextPrefix); return e
            } r.prototype.renderSeparator = function (e) {
                return React.createElement("span", { className: "separator", key: e }, "|")
            }; r.prototype.render = function () {
                var t = this; var r = e.Categories.groupByCategory(this.props.items); return React.createElement("div", { className: "category-links" }, r.inOrder.map(function (r, n) {
                    return [n > 0 && t.renderSeparator("sep-" + n), React.createElement(e.CategoryLink, { categoryId: "Category" + r.order, key: n }, t.text(r.key, "Categories." + r.key))]
                }))
            }; return r
        }(React.Component); e.CategoryLinks = t
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));
var Serenity; (function (e) {
    var t; (function (e) {
        var t = function (t) {
            __extends(r, t); function r(e, r) {
                var n = t.call(this, e, r) || this; n.text = Q.prefixedText(n.props.localTextPrefix); n.state = { collapsed: n.props.collapsed }; return n
            } r.prototype.componentWillReceiveProps = function (e) {
                if (e.collapsed !== this.props.collapsed) {
                    this.setState({ collapsed: e.collapsed })
                }
            }; r.prototype.getClassName = function () {
                if (this.state.collapsed == null) return "category "; if (this.state.collapsed == true) return "category collapsible collapsed"; return "category collapsible"
            }; r.prototype.getCategoryId = function () {
                if (!this.props.categoryId) return null; return Q.coalesce(this.props.idPrefix, "") + this.props.categoryId
            }; r.prototype.handleTitleClick = function () {
                if (this.state.collapsed == null) return; this.setState({ collapsed: !this.state.collapsed })
            }; r.prototype.renderTitle = function () {
                var t = this; if (this.props.category == null) return null; return React.createElement(e.CategoryTitle, {
                    categoryId: this.getCategoryId(), collapsed: this.state.collapsed, onClick: function () {
                        return t.handleTitleClick()
                    }
                }, this.text(this.props.category, "Categories." + this.props.category))
            }; r.prototype.renderField = function (t) {
                var r = Q.extend({ idPrefix: this.props.idPrefix, localTextPrefix: this.props.localTextPrefix, key: t.name, setRef: this.props.setRef }, t); if (this.props.renderField != null) {
                    var n = this.props.renderField(r); if (n !== undefined) return n
                } return React.createElement(e.PropertyField, r)
            }; r.prototype.renderWithBreak = function (t) {
                return [React.createElement(e.CategoryLineBreak, { breakClass: t.formCssClass, key: "break-" + t.name }), this.renderField(t)]
            }; r.prototype.render = function () {
                var e = this; var t = this.props; return React.createElement("div", { className: this.getClassName() }, this.renderTitle(), t.items && t.items.map(function (t) {
                    if (t.formCssClass && t.formCssClass.indexOf("line-break-") >= 0) return e.renderWithBreak(t); return e.renderField(t)
                }), t.children)
            }; return r
        }(React.Component); e.Category = t
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));
var Serenity; (function (e) {
    var t; (function (e) {
        var t = function () {
            function e() {
            } return e
        }(); e.CategoriesProps = t; var r = function (t) {
            __extends(r, t); function r() {
                return t !== null && t.apply(this, arguments) || this
            } r.applyOrder = function (e, t) {
                if (!t) return; var r = t.split(";"); var n = 0; var i = {}; for (var o = 0, a = r; o < a.length; o++) {
                    var s = a[o]; var l = Q.trimToNull(s); if (l == null || i[l] != null) continue; i[l] = n++
                } e.inOrder.sort(function (e, t) {
                    var r = i[e.key]; if (r == null) i[e.key] = i = n++; var o = i[t.key]; if (o == null) i[t.key] = i = n++; return r - o
                }); for (n = 0; n < e.inOrder.length; n++)e.inOrder[n].order = n
            }; r.groupByCategory = function (e, t, n) {
                var i = Q.coalesce(t, ""); var o = Q.groupBy(e || [], function (e) {
                    return Q.coalesce(e.category, i)
                }); r.applyOrder(o, n); return o
            }; r.prototype.renderCategory = function (t) {
                var r = { categoryId: "Category" + t.order, category: t.key, idPrefix: this.props.idPrefix, localTextPrefix: this.props.localTextPrefix, items: t.items, key: t.order, renderField: this.props.renderField, setRef: this.props.setRef }; if (this.props.renderCategory != null) {
                    var n = this.props.renderCategory(r); if (n !== undefined) return n
                } return React.createElement(e.Category, __assign({}, r))
            }; r.prototype.render = function () {
                var e = this; return React.createElement("div", { className: "categories" }, r.groupByCategory(this.props.items || [], this.props.defaultCategory, this.props.categoryOrder).inOrder.map(function (t) {
                    return e.renderCategory(t)
                }))
            }; return r
        }(React.Component); e.Categories = r
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));
var Serenity; (function (e) {
    var t; (function (t) {
        var r = function (t) {
            __extends(r, t); function r() {
                return t !== null && t.apply(this, arguments) || this
            } r.prototype.render = function () {
                var e = this; var t = this.props, r = t.options, n = __rest(t, ["options"]); return React.createElement("form", __assign({}, n, {
                    ref: function (t) {
                        e.form = t
                    }, onSubmit: this.props.onSubmit || function (t) {
                        return e.handleSubmit(t)
                    }
                }), this.props.children)
            }; r.prototype.handleSubmit = function (e) {
                if (!this.validator.valid()) return false
            }; r.prototype.componentDidMount = function () {
                this.validator = $(this.form).validate(Q.validateOptions(this.props.options))
            }; r.prototype.validateForm = function () {
                return this.validator == null || !!this.validator.form()
            }; r.prototype.serialize = function () {
                var t = {}; $(this.form).find(":input, .editor").each(function (r, n) {
                    var i = $(n).attr("name"); if (!i) return; var o = $(n).tryGetWidget(e.Widget); if (o) t[i] = e.EditorUtils.getValue(o); else t[i] = $(n).val()
                }); return t
            }; return r
        }(React.Component); t.ValidateForm = r
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));
var Serenity; (function (e) {
    var t; (function (e) {
        var t = function (e) {
            __extends(t, e); function t() {
                return e !== null && e.apply(this, arguments) || this
            } t.prototype.render = function () {
                return React.createElement("div", { className: "flex-layout" }, React.createElement("div", { className: "s-Form" }, e.prototype.render.call(this)))
            }; return t
        }(e.ValidateForm); e.Form = t
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));
var Serenity; (function (e) {
    var t; (function (e) {
        var t = function (t) {
            __extends(r, t); function r() {
                var e = t !== null && t.apply(this, arguments) || this; e.text = Q.prefixedText(e.props.localTextPrefix); return e
            } r.groupByTab = function (e) {
                return Q.groupBy(e || [], function (e) {
                    return Q.coalesce(e.tab, "")
                })
            }; r.prototype.renderTab = function (e) {
                return React.createElement("li", { className: e.order == 0 ? "active" : null, key: e.order }, React.createElement("a", { "data-toggle": "tab", role: "tab", href: "#" + this.props.idPrefix + "_Tab" + e.order }, this.text(e.key, "Tabs." + e.key)))
            }; r.prototype.renderPane = function (e) {
                return React.createElement("div", { id: this.props.idPrefix + "_Tab" + e.order, key: e.order, className: "tab-pane fade" + (e.order == 0 ? " in active" : ""), role: "tabpanel" }, this.renderCategories(e))
            }; r.prototype.renderCategories = function (t) {
                var r = { items: t.items, idPrefix: this.props.idPrefix, localTextPrefix: this.props.localTextPrefix, categoryOrder: this.props.categoryOrder, defaultCategory: this.props.defaultCategory, renderCategory: this.props.renderCategory, renderField: this.props.renderField, setRef: this.props.setRef }; if (this.props.renderCategories) {
                    var n = this.props.renderCategories(t.key, r); if (n !== undefined) return n
                } return React.createElement(e.Categories, __assign({}, r))
            }; r.prototype.render = function () {
                var e = this; var t = r.groupByTab(this.props.items || []); return React.createElement(React.Fragment, null, React.createElement("ul", { className: "nav nav-tabs property-tabs", role: "tablist" }, t.inOrder.map(function (t) {
                    return e.renderTab(t)
                })), React.createElement("div", { className: "tab-content property-panes" }, t.inOrder.map(function (t) {
                    return e.renderPane(t)
                })))
            }; return r
        }(React.Component); e.PropertyTabs = t
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));
var Serenity; (function (e) {
    var t; (function (e) {
        var t = function (t) {
            __extends(r, t); function r() {
                return t !== null && t.apply(this, arguments) || this
            } r.prototype.loadFrom = function (e, t) {
                var r = this.props.items || []; for (var n = 0, i = r; n < i.length; n++) {
                    var o = i[n]; if (this.props.mode != 2 && o.defaultValue != null && typeof e[o.name] === "undefined") {
                        e[o.name] = o.defaultValue
                    }
                } t.loadFrom(e, r.map(function (e) {
                    return e.name
                }))
            }; r.prototype.canModifyItem = function (e) {
                if (this.props.mode != 2) {
                    if (e.insertable === false) {
                        return false
                    } if (e.insertPermission == null) {
                        return true
                    } return Q.Authorization.hasPermission(e.insertPermission)
                } if (e.updatable === false) {
                    return false
                } if (e.updatePermission == null) {
                    return true
                } return Q.Authorization.hasPermission(e.updatePermission)
            }; r.prototype.saveTo = function (e, t) {
                t.saveTo(e)
            }; r.prototype.getItems = function () {
                var e = this; return (this.props.items || []).map(function (t) {
                    var r = e.canModifyItem(t); var n = !!t.oneWay || !r; var i = t.readOnly === true || !r; var o = !i && !!t.required && t.editorType !== "Boolean"; var a = !(t.readPermission != null && !Q.Authorization.hasPermission(t.readPermission) || t.visible === false || e.props.mode != 2 && t.hideOnInsert === true || e.props.mode == 2 && t.hideOnUpdate === true); return Q.extend(Q.extend({}, t), { readOnly: i, oneWay: n, required: o, visible: a })
                })
            }; r.prototype.render = function () {
                var t = Q.extend({}, this.props); t.items = this.getItems(); var r = Q.any(t.items, function (e) {
                    return !Q.isEmptyOrNull(e.tab)
                }); if (r) return React.createElement(e.PropertyTabs, t); var n = t.useCategories !== false && Q.any(t.items, function (e) {
                    return !Q.isEmptyOrNull(e.category)
                }); if (n) {
                    React.createElement(React.Fragment, null, React.createElement(e.CategoryLinks, t), React.createElement(e.Categories, t))
                } return React.createElement("div", { className: "categories" }, React.createElement(e.Category, t))
            }; return r
        }(React.Component); e.IntraPropertyGrid = t; var r = function (e) {
            __extends(t, e); function t() {
                return e !== null && e.apply(this, arguments) || this
            } t.prototype.render = function () {
                return React.createElement("div", { className: "s-PropertyGrid" }, e.prototype.render.call(this))
            }; return t
        }(t); e.PropertyGrid = r
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));
var Serenity; (function (e) {
    var t; (function (e) {
        var t = function (e) {
            __extends(t, e); function t() {
                return e !== null && e.apply(this, arguments) || this
            } t.prototype.render = function () {
                return React.createElement("div", { className: "align-right", style: { marginTop: "10px", paddingRight: "24px" } }, this.props.children)
            }; return t
        }(React.Component); e.ButtonPanel = t
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));
var Serenity; (function (e) {
    var t; (function (t) {
        var r = function (t) {
            __extends(r, t); function r() {
                return t !== null && t.apply(this, arguments) || this
            } n = r; r.adjustIconClass = function (e) {
                if (!e) return e; if (Q.startsWith(e, "fa-")) return "fa " + e; if (Q.startsWith(e, "glyphicon-")) return "glyphicon " + e; return e
            }; r.className = function (e) {
                var t; return Q.cssClass((t = { "tool-button": true, "icon-tool-button": !!e.icon, "no-text": !e.title, disabled: e.disabled }, t[e.cssClass] = !!e.cssClass, t))
            }; r.prototype.handleClick = function (e) {
                if (!this.props.onClick || $(e.currentTarget).hasClass("disabled")) return; this.props.onClick(e)
            }; r.prototype.render = function () {
                var e = this; return React.createElement("div", {
                    className: n.className(this.props), title: this.props.hint, onClick: function (t) {
                        return e.handleClick(t)
                    }
                }, React.createElement("div", { className: "button-outer" }, this.renderButtonText()))
            }; r.prototype.renderButtonText = function () {
                var e = this.props; var t = n.adjustIconClass(e.icon); if (!t && !e.title) return React.createElement("span", { className: "button-inner" }); var r = Q.coalesce(e.title, ""); if (e.htmlEncode === false) {
                    var i = (t ? '<i class="' + Q.attrEncode(t) + '"></i> ' : "") + r; return React.createElement("span", { className: "button-inner", dangerouslySetInnerHTML: { __html: i } })
                } if (!t) return React.createElement("span", { className: "button-inner" }, r); return React.createElement("span", { className: "button-inner" }, React.createElement("i", { className: t }), " ", r)
            }; var n; r.buttonSelector = "div.tool-button"; r = n = __decorate([e.Decorators.registerClass("Serenity.UI.ToolButton")], r); return r
        }(React.Component); t.ToolButton = r; var n = function (e) {
            __extends(r, e); function r() {
                return e !== null && e.apply(this, arguments) || this
            } r.prototype.setupMouseTrap = function () {
                var e = this; if (!window["Mousetrap"]) return; var r; for (var n = 0, i = this.props.buttons || []; n < i.length; n++) {
                    var o = i[n]; if (Q.isEmptyOrNull(o.hotkey)) continue; this.mouseTrap = this.mouseTrap || window["Mousetrap"](this.props.hotkeyContext || window.document.documentElement); (function (n) {
                        var i = (r = r || $(e.el).find(t.ToolButton.buttonSelector)).filter("." + n.cssClass); e.mouseTrap.bind(n.hotkey, function (e, t) {
                            if (i.is(":visible")) {
                                i.click()
                            } return n.hotkeyAllowDefault
                        })
                    })(o)
                }
            }; r.prototype.componentDidMount = function () {
                this.setupMouseTrap()
            }; r.prototype.componentWillUnmount = function () {
                $(this.el).find(t.ToolButton.buttonSelector).unbind("click"); if (this.mouseTrap) {
                    if (!!this.mouseTrap.destroy) {
                        this.mouseTrap.destroy()
                    } else { this.mouseTrap.reset() } this.mouseTrap = null
                }
            }; r.prototype.render = function () {
                var e = this; return React.createElement("div", {
                    className: "tool-buttons", ref: function (t) {
                        return e.el = t
                    }
                }, React.createElement("div", { className: "buttons-outer" }, this.renderButtons(this.props.buttons || [])))
            }; r.prototype.renderButtons = function (e) {
                var r = []; for (var n = 0, i = e; n < i.length; n++) {
                    var o = i[n]; if (o.separator) r.push(React.createElement("div", { className: "separator", key: r.length })); r.push(React.createElement(t.ToolButton, __assign({}, o, { key: r.length })))
                } var a = 0; return React.createElement("div", { className: "buttons-inner" }, r.map(function (e) {
                    e.key = ++a; return e
                }), this.props.children)
            }; return r
        }(React.Component); t.IntraToolbar = n; var i = function (t) {
            __extends(r, t); function r() {
                return t !== null && t.apply(this, arguments) || this
            } r.prototype.render = function () {
                return React.createElement("div", { className: "s-Toolbar clearfix" }, t.prototype.render.call(this))
            }; r = __decorate([e.Decorators.registerClass("Serenity.UI.Toolbar")], r); return r
        }(n); t.Toolbar = i
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));
var Serenity; (function (e) {
    var t; (function (e) {
        var t = function (t) {
            __extends(r, t); function r() {
                return t !== null && t.apply(this, arguments) || this
            } r.prototype.render = function () {
                var t = Q.text("Controls.EntityDialog." + (this.props.isUpdate ? "UpdateButton" : "SaveButton")); return React.createElement(e.ToolButton, __assign({ icon: "fa-save text-light-blue", title: t }, this.props))
            }; return r
        }(React.Component); e.SaveButton = t; var r = function (t) {
            __extends(r, t); function r() {
                return t !== null && t.apply(this, arguments) || this
            } r.prototype.render = function () {
                var t = Q.text("Controls.EntityDialog.ApplyChangesButton"); return React.createElement(e.ToolButton, __assign({ icon: "fa-save", cssClass: "text-black", hint: t }, this.props))
            }; return r
        }(React.Component); e.ApplyChangesButton = r; var n = function (t) {
            __extends(r, t); function r() {
                return t !== null && t.apply(this, arguments) || this
            } r.prototype.render = function () {
                var t = Q.text("Controls.EntityDialog.DeleteButton"); return React.createElement(e.ToolButton, __assign({ icon: "fa-times text-red", title: t }, this.props))
            }; return r
        }(React.Component); e.DeleteButton = n
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));
var Serenity; (function (e) {
    var t; (function (e) {
        var t; (function (e) {
            e[e["Initial"] = 0] = "Initial"; e[e["New"] = 1] = "New"; e[e["Edit"] = 2] = "Edit"; e[e["View"] = 3] = "View"; e[e["Deleted"] = 4] = "Deleted"
        })(t = e.FormMode || (e.FormMode = {}))
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));
var Serenity; (function (e) {
    var t; (function (t) {
        var r = function (r) {
            __extends(n, r); function n(e, t) {
                var n = r.call(this, e, t) || this; n.emptyEntity = Object.create(null); var i = n.props.entity; var o = n.modeFor(i); n.state = { formMode: o, formTitle: n.titleFor(i, o), entity: i }; if (n.props.entityId != null) n.loadById(n.props.entityId); n.delete = n.delete.bind(n); n.save = n.save.bind(n); return n
            } n.prototype.componentWillReceiveProps = function (e) {
                var t = Q.coalesce(e.entityId, null); var r = Q.coalesce(this.props.entityId, null); if (t !== r) {
                    if (t === null) {
                        this.loadEntity(e.entity || Object.create(null))
                    } else { this.loadById(e.entityId) }
                } else if (e.entity !== this.props.entity) {
                    this.loadEntity(Q.coalesce(e.entity, Object.create(null)))
                }
            }; n.prototype.componentDidMount = function () {
                this.canSetState = true; if (this.pendingEntity !== undefined) {
                    this.loadEntity(this.pendingEntity || Object.create(null))
                }
            }; n.prototype.componentWillUnmount = function () {
                this.canSetState = false
            }; n.prototype.loadEntity = function (e) {
                if (this.canSetState) {
                    var t = this.modeFor(e); this.setState({ formMode: t, formTitle: this.titleFor(e, t), entity: e }); this.pendingEntity = undefined
                } else { this.pendingEntity = e || null }
            }; n.prototype.loadResponse = function (e) {
                this.loadEntity(e.Entity)
            }; n.prototype.getLoadByIdRequest = function (e) {
                return { EntityId: e }
            }; n.prototype.getServiceFor = function (e) {
                var t = this.props[e.charAt(0).toLowerCase() + e.substr(1) + "Service"]; if (t != null) return t; return (this.props.service || "ServiceNotSet!") + "/" + e
            }; n.prototype.getLoadByIdOptions = function (e) {
                return { service: this.getServiceFor("Retrieve"), request: this.getLoadByIdRequest(e) }
            }; n.prototype.loadById = function (e) {
                var t = this; var r = this.getLoadByIdOptions(e); return Q.serviceCall(r).then(function (e) {
                    t.loadResponse(e); return e
                })
            }; n.prototype.isDeleted = function (e) {
                if (this.props.isDeletedProperty && e[this.props.isDeletedProperty]) return true; if (this.props.isActiveProperty && e[this.props.isActiveProperty] === -1) return true
            }; n.prototype.modeFor = function (e) {
                if (e == null) return t.FormMode.Initial; if (this.props.readOnly) return t.FormMode.View; if (this.props.idProperty && e[this.props.idProperty] != null) return t.FormMode.Edit; if (this.isDeleted(e)) return t.FormMode.Deleted; return t.FormMode.New
            }; n.prototype.getEntityName = function () {
                if (this.props.localTextPrefix) {
                    var e = Q.tryGetText("Db." + this.props.localTextPrefix + ".EntitySingular"); if (e) return e
                } return "Item"
            }; n.prototype.getNameValue = function (e) {
                if (!e || !this.props.nameProperty) return ""; return Q.coalesce(e[this.props.nameProperty], "").toString()
            }; n.prototype.titleFor = function (e, r) {
                if (r == t.FormMode.New) return Q.format(Q.text("Controls.EntityDialog.NewRecordTitle"), this.getEntityName()); var n = Q.coalesce(this.getNameValue(e), ""); return Q.format(Q.text("Controls.EntityDialog.EditRecordTitle"), this.getEntityName(), Q.isEmptyOrNull(n) ? "" : " (" + n + ")")
            }; n.prototype.isEditMode = function () {
                return this.state.formMode == t.FormMode.Edit
            }; n.prototype.getSaveEntity = function (e) {
                return e
            }; n.prototype.getSaveOptions = function (e) {
                var t = {}; t.service = this.getServiceFor(this.isEditMode() ? "Update" : "Create"); t.request = this.getSaveRequest(e); return t
            }; n.prototype.getIdProperty = function () {
                return this.props.idProperty || "NoIdProperty!"
            }; n.prototype.getLanguages = function () {
                if (e.EntityDialog.defaultLanguageList != null) return e.EntityDialog.defaultLanguageList() || []; return []
            }; n.prototype.getPendingLocalizations = function () {
                if (this.state.localizations == null) {
                    return null
                } var e = {}; var t = this.getIdProperty(); var r = this.getLanguages(); for (var n = 0, i = r; n < i.length; n++) {
                    var o = i[n]; var a = o[0]; var s = {}; if (t != null) {
                        s[t] = this.entity[this.getIdProperty()]
                    } var l = a + "$"; for (var p = 0, u = Object.keys(this.state.localizations); p < u.length; p++) {
                        var c = u[p]; if (Q.startsWith(c, l)) s[c.substr(l.length)] = this.state.localizations[c]
                    } e[a] = s
                } return e
            }; n.prototype.getSaveRequest = function (e) {
                var t = this.getSaveEntity(e); var r = {}; r.Entity = t; if (this.isEditMode()) {
                    var n = this.getIdProperty(); if (n != null) {
                        r.EntityId = this.entity[this.props.idProperty]
                    }
                } if (this.state.localizations != null) {
                    r.Localizations = this.getPendingLocalizations()
                } return r
            }; n.prototype.save = function (e) {
                var t = this.getSaveOptions(e); return Q.serviceCall(t)
            }; n.prototype.delete = function () {
                return null
            }; n.prototype.undelete = function () {
                return null
            }; n.prototype.getDataModel = function () {
                return { entity: this.state.entity || this.emptyEntity, formMode: this.state.formMode, formTitle: this.state.formTitle, onSave: this.state.formMode == t.FormMode.Edit || this.state.formMode == t.FormMode.New ? this.save : null, onDelete: this.state.formMode == t.FormMode.Edit ? this.delete : null, onUndelete: this.state.formMode == t.FormMode.Deleted ? this.undelete : null }
            }; Object.defineProperty(n.prototype, "dataModel", {
                get: function () {
                    return this.getDataModel()
                }, enumerable: true, configurable: true
            }); Object.defineProperty(n.prototype, "entity", {
                get: function () {
                    return this.pendingEntity !== undefined ? this.pendingEntity : this.state.entity
                }, enumerable: true, configurable: true
            }); n.prototype.render = function () {
                return this.props.view(this.getDataModel())
            }; return n
        }(React.Component); t.FormDataSource = r
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));
var Serenity; (function (e) {

    var t; (function (e) {

        var t = function (t) {

            __extends(r, t); function r() {
                var r = t !== null && t.apply(this, arguments) || this; r.editors = new e.EditorRefs; return r
            } r.prototype.canSave = function () {
                return this.props.onSave != null
            }; r.prototype.showSave = function () {
                return this.props.formMode == e.FormMode.Edit || this.props.formMode == e.FormMode.New
            }; r.prototype.canClose = function () {
                return this.props.onClose != null
            }; r.prototype.showApplyChanges = function () {
                return this.showSave() && this.canClose()
            }; r.prototype.isUpdate = function () {
                return this.props.formMode == e.FormMode.Edit
            }; r.prototype.canDelete = function () {
                return this.props.onDelete != null
            }; r.prototype.showDelete = function () {
                return this.props.formMode == e.FormMode.Edit
            }; r.prototype.canUndelete = function () {
                return this.props.onUndelete != null
            }; r.prototype.showUndelete = function () {
                return this.props.formMode == e.FormMode.Deleted
            }; r.prototype.loadEntity = function (e) {

                this.editors.loadFrom(e)
            }; r.prototype.componentDidMount = function () {
                this.loadEntity(this.props.entity || Object.create(null))
            }; r.prototype.componentWillReceiveProps = function (e) {
                if (e.entity !== null && this.props.entity !== e.entity) {
                    this.loadEntity(e.entity || Object.create(null))
                }
            }; r.prototype.renderSaveButton = function () {
                var t = this; return React.createElement(e.SaveButton, {
                    isUpdate: this.isUpdate(), disabled: !this.canSave(), onClick: function () {
                        return t.save(true)
                    }
                })
            }; r.prototype.renderApplyChangesButton = function () {
                var t = this; return React.createElement(e.ApplyChangesButton, {
                    disabled: !this.canSave(), onClick: function () {
                        return t.save(false)
                    }
                })
            }; r.prototype.renderDeleteButton = function () {
                return React.createElement(e.DeleteButton, { disabled: !this.canDelete() })
            }; r.prototype.renderToolbar = function (t) {
                return React.createElement(e.Toolbar, null, this.showSave() && this.renderSaveButton(), this.showApplyChanges() && this.renderApplyChangesButton(), this.showDelete() && this.renderDeleteButton(), t)
            }; r.prototype.save = function (e) {
                var t = this; if (this.props.onSave == null) return Promise.reject("No onSave handler!"); var r = Object.create(null); this.editors.saveTo(r); var n = this.props.onSave(r); if (e) {
                    if (this.props.onClose != null) n = n.then(function (e) {
                        return t.props.onClose != null && t.props.onClose()
                    })
                } else if (this.props.onReload != null) {
                    n = n.then(function (e) {
                        return t.props.onReload != null && t.props.onReload()
                    })
                } return n
            }; return r
        }(React.Component); e.FormView = t
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));


var Serenity; (function (e) {

    var t; (function (t) {

        var r = function (e) {

            __extends(t, e); function t(t) {

                var r = e.call(this, t) || this; r.mountView(); return r
            } t.prototype.loadByIdAndOpenDialog = function (e, t) {

                this.entityId = e; this.dialogOpen(t); this.mountView()
            }; t.prototype.loadEntityAndOpenDialog = function (e, t) {

                this.entityId = null; this.entity = e; this.dialogOpen(t); this.mountView()
            }; t.prototype.loadNewAndOpenDialog = function (e) {

                this.entityId = null; this.entity = {}; this.dialogOpen(e); this.mountView()
            }; t.prototype.mountView = function () {

                if (!this.element || !this.element.length) return;
                var e = Q.extend({}, this.options); this.view = ReactDOM.render(this.render(), this.element[0])
            }; t.prototype.getTemplate = function () {
                return '<div class="s-DialogContent"></div>'
            }; return t
        }(e.TemplatedDialog); t.Dialog = r
    })(t = e.UI || (e.UI = {}))
})(Serenity || (Serenity = {}));

;


var Serenity; (function (t) {

    var e = function () {

        function e() {
        } i = e; e.prototype.format = function (t) {

            return i.formatValue(t.value)
        }; e.formatValue = function (t) {

            var e = $("<div/>").html(t || "").text(); return Q.toSingleLine(e)
        };
        var i; e = i = __decorate([t.Decorators.registerFormatter("Serenity.SingleLineTextFormatter")], e); return e
    }(); t.SingleLineTextFormatter = e
})(Serenity || (Serenity = {}));

var Serenity; (function (t) {

    var e = function () {

        function t(e) {
            var i = this; this.options = $.extend({}, t.defaults, e); localStorage.setItem("IdleTimeout:LastActivity", $.now().toString()); localStorage.removeItem("IdleTimeout:Signout"); var n, r; $("body").on(this.options.activityEvents, function (t) {
                if (t.type == "mousemove") {
                    if (t.clientX == n && t.clientY == r) return; n = t.clientX; r = t.clientY
                } if (!i.isWarningActive()) {
                    i.resetIdleTimer()
                }
            }); if (this.options.keepAliveTimer) this.startKeepAliveTimer(); this.resetIdleTimer()
        } t.prototype.startKeepAliveTimer = function () {
            var t = this; clearTimeout(this.keepAliveTimer); var e = function () {
                $.get(Q.resolveUrl(t.options.keepAliveUrl) + "?" + (new Date).getTime()); t.startKeepAliveTimer()
            }; this.keepAliveTimer = setTimeout(e, this.options.keepAliveTimer * 1e3)
        }; t.prototype.isWarningActive = function () {
            return $("#idle-timeout-warning").is(":visible")
        }; t.prototype.startWarningTimer = function () {
            var t = this; this.warningTimer = setInterval(function () {
                return t.warningTimeout()
            }, this.options.checkInterval * 1e3)
        }; t.prototype.stopWarningTimer = function () {
            clearInterval(this.warningTimer); clearInterval(this.remainingTimer)
        }; t.prototype.warningTimeout = function () {
            if (this.getRemainingSeconds() < 0) this.signout()
        }; t.prototype.resetIdleTimer = function () {
            clearTimeout(this.idleTimer); localStorage.setItem("IdleTimeout:LastActivity", $.now().toString()); this.idleTimeout()
        }; t.prototype.idleTimeout = function () {
            var t = this; this.checkActivityTimeout(); this.idleTimer = setTimeout(function () {
                return t.idleTimeout()
            }, this.options.checkInterval * 1e3)
        }; t.prototype.checkActivityTimeout = function () {
            var t = parseInt(localStorage.getItem("IdleTimeout:LastActivity") || $.now().toString()) + (this.options.activityTimeout - (this.options.warningDuration || 0)) * 1e3; if ($.now() > t) {
                if (!this.options.warningDuration) {
                    this.signout()
                } else { this.showWarning(); this.startWarningTimer() }
            } else if (localStorage.getItem("IdleTimeout:Signout")) {
                this.signout()
            } else if (this.isWarningActive()) {
                this.closeWarning(); this.stopWarningTimer()
            }
        }; t.prototype.signout = function () {
            localStorage.setItem("IdleTimeout:Signout", "1"); clearTimeout(this.keepAliveTimer); this.stopWarningTimer(); this.closeWarning(); this.options.beforeSignout && this.options.beforeSignout(); if (this.options.signoutUrl) window.location.href = Q.resolveUrl(this.options.signoutUrl)
        }; t.prototype.showWarning = function () {
            var t = this; var e = $("#idle-timeout-warning"); if (!e.length) {
                e = $('<div class="modal fade" id="idle-timeout-warning">\n    <div class="modal-dialog">\n        <div class="modal-content">\n            <div class="modal-header">\n            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>\n            <h4 class="modal-title">' + Q.text("Controls.IdleTimeout.WarningTitle") + ' </h4>\n            </div>\n            <div class="modal-body">\n            <p>' + Q.text("Controls.IdleTimeout.WarningMessage") + "</p>\n            <p>" + Q.text("Controls.IdleTimeout.CountdownMessage") + '</p>\n            <div class="progress">\n                <div id="idle-timeout-progress" class="progress-bar progress-bar-striped active" role="progressbar" \n                    style="min-width: 15px; width: 100%;">\n                </div>\n            </div>\n            <div class="modal-footer">\n                <button id="idle-timeout-warning-signout" type="button" class="btn btn-default">\n                    ' + Q.text("Controls.IdleTimeout.SignoutButton") + '\n                </button>\n                <button id="idle-timeout-warning-keepalive" type="button" class="btn btn-primary" data-dismiss="modal">\n                    ' + Q.text("Controls.IdleTimeout.KeepAliveButton") + "\n                </button>\n            </div>\n        </div>\n    </div>\n</div>").appendTo(document.body); $("#idle-timeout-warning-signout").on("click", function () {
                    return t.signout()
                }); e.on("hide.bs.modal", function () {
                    clearTimeout(t.warningTimer); clearTimeout(t.remainingTimer); t.resetIdleTimer()
                })
            } e["modal"]("show"); this.countdownDisplay()
        }; t.prototype.closeWarning = function () {
            var t = $("#idle-timeout-warning").off("hide.bs.modal"); if (t.length && t.data() && t.data()["bs.modal"] && t.data()["bs.modal"].isShown) {
                t.removeClass("fade"); t["modal"]("hide"); t.remove()
            }
        }; t.prototype.getRemainingSeconds = function () {
            var t = parseInt(localStorage.getItem("IdleTimeout:LastActivity") || $.now().toString()) + this.options.activityTimeout * 1e3; return Math.ceil((t - $.now()) / 1e3)
        }; t.prototype.countdownDisplay = function () {
            var t = this; clearTimeout(this.remainingTimer); var e = function () {
                var e = Math.max(t.getRemainingSeconds(), 1); var i = 100 * Math.max(1, e) / (t.options.warningDuration || 1); $("#idle-timeout-progress").css("width", i + "%"); $("#idle-timeout-seconds").text(e)
            }; this.remainingTimer = setInterval(e, 500); e()
        }; t.defaults = { activityEvents: "click keyup mouseup mousemove scroll touchend touchmove wheel mousewheel", activityTimeout: 900, checkInterval: 5, keepAliveTimer: 300, keepAliveUrl: "~/Account/KeepAlive", signoutUrl: "~/Account/Signout", warningDuration: 60 }; return t
    }(); t.IdleTimeout = e
})(Serenity || (Serenity = {}));

var Serenity; (function (t) {

    function e() {

        t.EntityGrid.prototype["getButtons"] = function (t) {

            return function () {

                var e = t.call(this); var i = Q.tryFirst(e, function (t) {

                    return t.cssClass === "add-button"
                });
                if (i) i.icon = "fa-plus-circle text-green";
                var n = Q.tryFirst(e, function (t) {

                    return t.cssClass === "refresh-button"
                });
                if (n) n.icon = "fa-refresh text-blue"; var r = Q.tryFirst(e, function (t) {

                    return t.cssClass === "column-picker-button"
                });
                if (r) r.icon = "fa-th-list text-blue"; var o = Q.tryFirst(e, function (t) {

                    return t.cssClass === "export-xlsx-button"
                }); if (o) o.icon = "fa-file-excel-o";
                return e
            }
        }(t.EntityGrid.prototype["getButtons"]);
        t.EntityDialog.prototype["getToolbarButtons"] = function (t) {

            return function () {

                var e = t.call(this); var i = Q.tryFirst(e, function (t) {

                    return t.cssClass === "save-and-close-button"
                });
                if (i) i.icon = "fa-floppy-o text-purple"; var n = Q.tryFirst(e, function (t) {

                    return t.cssClass === "apply-changes-button"
                }); if (n) n.icon = "fa-check-circle text-purple";
                var r = Q.tryFirst(e, function (t) {

                    return t.cssClass === "delete-button"
                });
                if (r) r.icon = "fa-trash-o text-red";
                var o = Q.tryFirst(e, function (t) {

                    return t.cssClass === "clone-button"
                });
                if (o) o.icon = "fa-clone";
                return e
            }
        }(t.EntityDialog.prototype["getToolbarButtons"]);
        t.EntityDialog.prototype["updateInterface"] = function (t) {

            return function () {

                t.call(this);
                if (this.saveAndCloseButton) {

                    this.saveAndCloseButton.find(".button-inner").html("<i class='fa fa-floppy-o text-purple'></i> " + Q.htmlEncode(Q.text(this.isNew() ? "Controls.EntityDialog.SaveButton" : "Controls.EntityDialog.UpdateButton")))
                }
            }
        }(t.EntityDialog.prototype["updateInterface"]);
        $.datepicker.setDefaults({
            buttonImage: null, buttonImageOnly: false, buttonText: '<i class="fa fa-calendar"></i>'
        });
        t.Toolbar.prototype["createButton"] = function (t) {

            return function (e, i) {

                if (i.separator) e = $('<div class="buttons-inner"></div>').appendTo(e.parent());
                else e = e.parent().children().last();
                t.call(this, e, i);
                if (i.separator) e.children().first().remove()
            }
        }(t.Toolbar.prototype["createButton"]);
        t.ColumnPickerDialog.prototype["createLI"] = function (t) {

            return function (t) {

                var e = this.allowHide(t);
                return $('\n<li data-key="' + t.id + '" class="' + (e ? "" : "cant-hide") + '">\n  <span class="drag-handle">☰</span>\n  ' + Q.htmlEncode(this.getTitle(t)) + "\n  " + (e ? '<i class="js-hide fa fa-eye-slash" title="' + Q.text("Controls.ColumnPickerDialog.HideHint") + '"></i>' : "") + '\n  <i class="js-show fa fa-eye" title="' + Q.text("Controls.ColumnPickerDialog.ShowHint") + '"></i>\n</li>')
            }
        }(t.ColumnPickerDialog.prototype["createLI"]); t.ColumnPickerDialog.prototype["getTemplate"] = function (t) {

            return function () {

                var e = t.call(this);
                return e.replace("fa-list", "fa-eye-slash")
            }
        }(t.ColumnPickerDialog.prototype["getTemplate"]); t.DataGrid.defaultRowHeight = 27; t.DataGrid.defaultHeaderHeight = 27
    } t.setupUIOverrides = e
})(Serenity || (Serenity = {}));

var Serenity;
(function (t) {

    var e = function (e) {

        __extends(i, e); function i(i) {

            var n = e.call(this, i) || this; n._step = 1; n.element.children().addClass("wizard-horz"); n.wizardGrid = new t.PropertyGrid(n.byId("WizardGrid"), n.getPropertyGridOptions()); n.wizardGrid.element.children(".property-tabs").children("li").each(function (t, e) {
                var i = $(e); var n = i.children("a").html(); i.html("").attr("data-step", t + 1); $('<span class="badge"/>').text(t + 1).appendTo(i); $('<span class="text"/>').html(n).appendTo(i); $('<span class="chevron"/>').appendTo(i)
            }); n.byId("CancelButton").click(function (t) {
                return n.confirmCancel(t)
            }); n.byId("BackButton").click(function () {
                n.moveToStep(n.step - 1)
            }); n.byId("NextButton").click(function () {
                if (!n.validateForm()) return false; n.moveToStep(n.step + 1)
            }); n.wizardGrid.element.children(".property-tabs").on("click", "li", function (t) {
                var e = $(t.currentTarget).data("step"); if (e > n.step) return; n.moveToStep(e)
            }); n.wizardGrid.load(n.getInitialEntity()); n.element.on("dialogbeforeclose panelbeforeclose", function (e) {
                if (!t.WX.hasOriginalEvent(e)) return; n.confirmCancel(e)
            }); return n
        } i.prototype.getPropertyGridOptions = function () {

            return { idPrefix: this.idPrefix, mode: 1, items: this.getPropertyItems(), localTextPrefix: this.getLocalTextPrefix(), useCategories: true }
        }; i.prototype.getFormKey = function () {
            return null
        }; i.prototype.getLocalTextPrefix = function () {
            return ""
        }; i.prototype.getPropertyItems = function () {
            var t = this.getFormKey(); if (t) return Q.getForm(t); return []
        }; i.prototype.getInitialEntity = function () {
            return {}
        }; Object.defineProperty(i.prototype, "maxSteps", {
            get: function () {
                return this.wizardGrid.element.children(".property-tabs").children("li").length
            }, enumerable: true, configurable: true
        }); i.prototype.moveToStep = function (t) {
            if (t == this.step || t < 1 || t > this.maxSteps + 1) return; if (t >= this.maxSteps + 1) {
                this.finish(); return
            } (t < this.step ? this.back : this.next).call(this, t)
        }; Object.defineProperty(i.prototype, "step", {
            get: function () {
                return this._step
            }, set: function (t) {
                this.getStepLink(this._step).removeClass("in active"); this.getStepPanel(this._step).removeClass("in active"); this.getStepLink(t).addClass("in active"); this.getStepPanel(t).addClass("in active").find(".require-layout").triggerHandler("layout"); var e; for (e = t; e <= this._step; e++)this.getStepLink(e).removeClass("complete").find("span.badge").removeClass("badge-success"); for (e = this._step; e < t; e++)this.getStepLink(e).addClass("complete").find("span.badge").addClass("badge-success"); this._step = t; this.byId("BackButton").toggleClass("disabled", this._step <= 1); this.byId("NextButton").children(".txt").text(Q.text("Site.WizardDialog." + (this._step == this.maxSteps ? "Finish" : "Next") + "Button"))
            }, enumerable: true, configurable: true
        }); i.prototype.reset = function () {
            this.step = 1; this.wizardGrid.load(this.getInitialEntity())
        }; i.prototype.finish = function () {
            this.dialogClose()
        }; i.prototype.next = function (t) {
            this.step = t
        }; i.prototype.back = function (t) {
            this.step = t
        }; i.prototype.getStepLink = function (t) {
            return this.wizardGrid.element.children(".property-tabs").children("li").eq(t - 1)
        }; i.prototype.getStepPanel = function (t) {
            return this.wizardGrid.element.children(".property-panes").children().eq(t - 1)
        }; i.prototype.getFallbackTemplate = function () {
            return '\n<div class="s-Form">\n    <form id="~_Form" action="">\n        <div id="~_WizardGrid" class="wizard-grid">\n        </div>\n\n        <div class="buttons" style="text-align: right; margin: 10px 20px;">\n            <button id="~_CancelButton" class="btn btn-danger" formnovalidate><i class="fa fa-ban"></i> Cancel</button>\n            <button id="~_BackButton" class="btn btn-primary disabled" formnovalidate><i class="fa fa-chevron-left"></i> ' + Q.text("Site.WizardDialog.BackButton") + '</button>\n            <button id="~_NextButton" class="btn btn-primary" formnovalidate><span class="txt">' + Q.text("Site.WizardDialog.NextButton") + '</span> <i class="fa fa-chevron-right"></i></button>\n        </div>\n    </form>\n</div>'
        }; i.prototype.getCancelMessage = function () {
            return Q.text("Site.WizardDialog.CancelMessage")
        }; i.prototype.confirmCancel = function (t) {
            var e = this; var i = this.getCancelMessage(); if (!i) {
                t.preventDefault(); this.dialogClose(); return
            } t.preventDefault(); Q.confirm(i, function () {
                e.dialogClose()
            })
        }; i.prototype.getSaveEntity = function (t) {
            var e = {}; this.wizardGrid.save(e); if (t != null) {
                var i = this.maxSteps; for (var n = 1; n <= i; n++) {
                    if (t.indexOf(n) >= 0) continue; var r = this.getStepPanel(n); r.find(".field").each(function (t, i) {
                        var n = $(i).attr("class").split(" ")[1]; if (n) delete e[n]
                    })
                }
            } return e
        }; Object.defineProperty(i.prototype, "compactSteps", {
            get: function () {
                return this.element.children().hasClass("compact-steps")
            }, set: function (t) {
                this.element.children().toggleClass("compact-steps", t)
            }, enumerable: true, configurable: true
        }); return i
    }(t.TemplatedDialog); t.WizardDialog = e
})(Serenity || (Serenity = {}));

var Serenity; (function (t) {

    var e = function (e) {

        __extends(n, e); function n(t) {

            var i = e.call(this, t.grid.element, t) || this; i.autoSizing = 0; i.columnResizeHandler = function () {
                if (!i.autoSizing && Q.coalesce(i.options.disableIfUserResize, true)) i.disabled = true
            }; i.queueAutoSize = 0; if (t.disabled != null) i.disabled = !!t.disabled; i.slickGrid = t.grid.slickGrid; i.$container = $(i.slickGrid.getContainerNode()); i.$container.on("dblclick." + i.uniqueName, ".slick-resizable-handle", function (t) {
                return i.resizeHandleClick(t)
            }); i.slickGrid.onColumnsResized.subscribe(i.columnResizeHandler); var n = i; i.oldMarkupReady = t.grid["markupReady"]; t.grid["markupReady"] = function () {
                n.oldMarkupReady && n.oldMarkupReady.call(this); if (!n.disabled && (!n.markupReadyOnce || !n.options.autoSizeAllJustOnce)) n.autoSizeAll(n.disabled != null ? false : null); n.markupReadyOnce = true
            }; i.oldPersistSettings = t.grid["persistSettings"]; t.grid["persistSettings"] = function (t) {
                if (n.autoSizing) return; n.oldPersistSettings.call(this, t)
            }; i.oldGetCurrentSettings = t.grid["getCurrentSettings"]; t.grid["getCurrentSettings"] = function (t) {
                t = t || this.gridPersistanceFlags(); t.columnWidths = Q.coalesce(t.columnWidths, !!this.disabled); return n.oldGetCurrentSettings.call(this, t)
            }; i.oldRestoreSettings = t.grid["restoreSettings"]; t.grid["restoreSettings"] = function (t, e) {
                if (t == null) t = this.getPersistedSettings(); e = e || this.gridPersistanceFlags(); if (t != null && t.columns && (e.columnWidths || Q.any(t.columns, function (t) {
                    return t.width != null
                }))) n.disabled = true; n.oldRestoreSettings.call(this, t, e)
            }; if (!i.disabled) i.autoSizeAll(); return i
        } n.prototype.destroy = function () {
            if (this.options && this.options.grid) {
                this.options.grid.element.off("." + this.uniqueName); this.options.grid["markupReady"] = this.oldMarkupReady
            } this.slickGrid && this.slickGrid.onColumnsResized.unsubscribe(this.columnResizeHandler); this.options = null; this.slickGrid = null; this.$container = null; e.prototype.destroy.call(this)
        }; n.prototype.resizeHandleClick = function (t) {
            if (t.ctrlKey) {
                t.preventDefault(); t.stopPropagation(); this.autoSizeAll(false); this.disabled = false; return
            } var e = $(t.currentTarget).closest(".slick-header-column"); var i = e.data("column"); if (!i || !i.resizable) return; t.preventDefault(); t.stopPropagation(); this.autoSizeColumn(e, i)
        }; n.prototype.getAutoColumnWidth = function (t, e) {
            var n = this.slickGrid.getColumnIndex(e.id); var r = this.getMaxColumnWidth(e, n); if (typeof this.options.ignoreHeaders == "number") {
                r = Math.max(Math.min(this.options.ignoreHeaders, i(t[0])), r)
            } else if (!this.options.ignoreHeaders) {
                r = Math.max(i(t[0]), r)
            } r = (r || 0) + 1; if (e.maxWidth > 0) r = Math.min(r, e.maxWidth); else r = Math.min(r, this.options.maxWidth || 400); if (e.minWidth > 0) r = Math.max(r, e.minWidth); else r = Math.max(r, this.options.minWidth || 30); return r
        }; n.prototype.autoSizeColumn = function (t, e) {
            var i = this.getAutoColumnWidth(t, e); if (i !== e.width) {
                this.autoSizing++; try { e.width = i; this.slickGrid.setColumns(this.slickGrid.getColumns()); this.slickGrid.onColumnsResized.notify() } finally { this.autoSizing-- }
            }
        }; n.prototype.autoSizeAll = function (e) {
            var i = this; if (!this.$container.is(":visible")) {
                if (this.queueAutoSize <= 0) {
                    this.queueAutoSize++; t.LazyLoadHelper.executeOnceWhenShown(this.$container, function () {
                        return i.autoSizeAll(e)
                    })
                } return
            } if (this.queueAutoSize > 0) {
                this.queueAutoSize--
            } this.autoSizing++; try {
                var n = this.$container.find(".slick-header-column"); e = Q.coalesce(e, this.options.useExplicitWidths); n.each(function (t, n) {
                    var r = $(n).data("column"); if (!r) return; var o = e && r.sourceItem && r.sourceItem.width > 0 && r.sourceItem.widthSet ? r.sourceItem.width : i.getAutoColumnWidth($(n), r); r.width = o
                }); this.slickGrid.setColumns(this.slickGrid.getColumns()); this.slickGrid.onColumnsResized.notify()
            } finally { this.autoSizing-- }
        }; n.prototype.createRow = function (t) {
            var e = $('<div class="slick-row"><div class="slick-cell" style="text-overflow: initial; white-space: nowrap;"></div></div>'); var i = this.slickGrid.getCanvasNode(); $(i).append(e); return e
        }; n.prototype.getMaxColumnWidth = function (t, e) {
            var i = this.createRow(t); var n = t.maxWidth > 0 ? t.maxWidth : this.options.maxWidth; try {
                var r = i.children()[0]; var o = this.slickGrid.getDataLength(); if (this.options.maxRows != null && this.options.maxRows < o) o = this.options.maxRows; var a = 0; var s = 0; for (var l = 0; l < o; l++) {
                    var u = this.slickGrid.getDataItem(l); if (t.formatter) {
                        try { var d = t.formatter(-1, e, u[t.field], t, u); r.innerHTML = d } catch (c) {
                            r.innerText = Q.coalesce(u[t.field], "")
                        } var p = r.childNodes[0]; if (p && p.tagName == "INPUT" && p.type != "checkbox") {
                            var h = getComputedStyle(p); s = (parseFloat(h.borderLeftWidth) || 0) + (parseFloat(h.borderRightWidth) || 0) + (parseFloat(h.paddingLeft) || 0) + (parseFloat(h.paddingRight) || 0); r.innerText = p.value
                        }
                    } else r.innerText = Q.coalesce(u[t.field], ""); var f = r.offsetWidth + s; if (f > a) a = f; if (n > 0 && f >= n) break
                } return a || 0
            } finally { i.remove() }
        }; return n
    }(t.Widget); t.AutoColumnWidthMixin = e; function i(t) {
        var e, i = t.cloneNode(true); i.style.cssText = "position: absolute; visibility: hidden; right: auto; text-overflow: initial; white-space: nowrap;"; t.parentNode.insertBefore(i, t); e = i.offsetWidth; i.parentNode.removeChild(i); return e
    }
})(Serenity || (Serenity = {}));

var Serenity; (function (t) {
    var e = function (t) {
        __extends(e, t); function e() {
            return t !== null && t.apply(this, arguments) || this
        } e.prototype.render = function () {
            var t = this; return React.createElement("div", { className: "card-items" }, this.props.items.map(function (e, i) {
                return React.createElement("div", { className: "card-item", key: i }, t.props.renderItem(e, i))
            }))
        }; return e
    }(React.Component); t.CardViewItems = e
})(Serenity || (Serenity = {}));
var Serenity; (function (t) {
    var e = function () {
        function e(t) {
            var e = this; this.options = t; var i = this.dataGrid = t.grid; var n = i.getIdProperty(); var r = this.getId = function (t) {
                return t[n]
            }; var o = $('\n<div class="btn-group view-switch" data-toggle="buttons" style="float: right">\n    <label class="btn btn-default active" title="' + Q.text("Site.CardViewMixin.ListView") + '">\n        <i class="fa fa-th-list text-purple"></i>\n        <input type="radio" name="' + i.element.attr("id") + '_ViewType" value="list" checked />\n    </label>\n    <label class="btn btn-default" title="' + Q.text("Site.CardViewMixin.CardView") + '">\n        <i class="fa fa-th-large text-purple"></i>\n        <input type="radio" name="' + i.element.attr("id") + '_ViewType" value="card" />    \n    </label>\n</div>').prependTo(i.element.find(".grid-title")); this.cardContainer = $('<div class="card-container" style="display: none;"></div>').insertAfter(i.element.children(".grid-container")); o.find("input").change(function (t) {
                return e.switchView($(t.target).val())
            }); this.resizeCardView(); i.element.bind("layout", function () {
                return e.resizeCardView()
            }); i.view.onDataChanged.subscribe(function () {
                e.updateCardItems()
            }); var a = i.getCurrentSettings; i.getCurrentSettings = function (t) {
                var e = a.apply(i, [t]); e["viewType"] = o.find("input:checked").val(); return e
            }; var s = i.restoreSettings; i.restoreSettings = function (t, e) {
                s.apply(i, [t, e]); if (t == null) {
                    var n = this.getPersistanceStorage(); if (n == null) return; var r = Q.trimToNull(n.getItem(this.getPersistanceKey())); if (!r) return; t = JSON.parse(r)
                } var a = t.viewType || "list"; var l = o.find("input:checked").val() || "list"; if (a != l) {
                    o.find("input").eq(a == "card" ? 1 : 0).click()
                }
            }
        } e.prototype.switchView = function (t) {
            this.resizeCardView(); var e = t == "card"; this.dataGrid.element.children(".card-container").toggle(e); this.dataGrid.element.children(".grid-container").toggle(!e); if (e) this.updateCardItems(); this.dataGrid.persistSettings()
        }; e.prototype.updateCardItems = function () {
            ReactDOM.render(React.createElement(t.CardViewItems, { items: this.dataGrid.getItems(), renderItem: this.options.renderItem }), this.cardContainer[0])
        }; e.prototype.resizeCardView = function () {
            var t = this.dataGrid.element.children(".grid-container"); var e = this.dataGrid.element.width(); var i = t.height(); this.dataGrid.element.children(".card-container").css({ width: e + "px", height: i + "px" })
        }; return e
    }(); t.CardViewMixin = e; Q.initFullHeightGridPage = function (t) {
        $("body").addClass("full-height-page"); t.addClass("responsive-height"); var e = function () {
            var e = t.parent().hasClass("page-content") || t.parent().is("section.content"); if (e) {
                t.css("height", "1px").css("overflow", "hidden")
            } var i = t.children(".card-container"); if (i.length && i.is(":visible")) {
                i.hide(); t.children(".grid-container").show(); try { Q.layoutFillHeight(t); t.triggerHandler("layout") } finally { t.children(".grid-container").hide(); i.show() }
            } else {
                Q.layoutFillHeight(t); if (e) {
                    t.css("overflow", "")
                } t.triggerHandler("layout")
            }
        }; if ($("body").hasClass("has-layout-event")) {
            $("body").bind("layout", e)
        } else if (window.Metronic) {
            window.Metronic.addResizeHandler(e)
        } else { $(window).resize(e) } e(); Q.Router.resolve()
    }
})(Serenity || (Serenity = {}));
var Serenity; (function (t) {
    var e = function () {
        function t(t) {
            var e = this; this.options = t; var i = this.dataGrid = t.grid; var n = i.getIdProperty(); var r = this.getId = function (t) {
                return t[n]
            }; var o = $('\n<div class="dropdown favorite-views" style="float: right">\n  <button class="btn btn-default dropdown-toggle" type="button" id="' + i.element.attr("id") + '_Favorites" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">\n    <i class="fa fa-star text-blue"></i> ' + Q.text("Site.FavoriteViewsMixin.FavoriteViews") + '\n    <span class="caret"></span>\n  </button>\n  <ul class="dropdown-menu" aria-labelledby="dropdownMenu1">\n    <li class="save">\n        <div>' + Q.text("Site.FavoriteViewsMixin.SaveView") + '</div>\n        <div><input type="text"></div>\n        <div><button class="btn btn-primary save-button"><i class="fa fa-floppy-o"></i> ' + Q.text("Site.FavoriteViewsMixin.SaveButton") + "</button></div>\n    </li>\n  </ul>\n</div>\n").prependTo(i.element.find(".grid-title")); o.on("shown.bs.dropdown", function (t) {
                o.find("input[type=text]").focus()
            }); var a = o.find("li.save input[type=text]"); o.find("button.save-button").click(function () {
                var t = Q.trimToNull(a.val()); if (!t) {
                    Q.notifyWarning(Q.text("Site.FavoriteViewsMixin.EmptyNameError")); return
                } var n = e.getFavorites(); n[t] = i.getCurrentSettings(); e.saveFavorites(n); Q.notifySuccess(Q.format(Q.text("Site.FavoriteViewsMixin.SaveSuccessMessage"), t)); a.val(""); e.populateFavorites()
            }); this.ul = o.children("ul"); this.ul.on("click", "i.fa-trash-o", function (t) {
                var i = $(t.target).closest("li").children("a").text(); var n = e.getFavorites(); delete n[i]; e.saveFavorites(n); e.populateFavorites(); Q.notifyWarning(Q.format(Q.text("Site.FavoriteViewsMixin.DeleteSuccessMessage"), i))
            }); this.ul.on("click", "a", function (t) {
                var n = $(t.target).text(); var r = e.getFavorites(); var o = r[n]; if (o) {
                    i.restoreSettings(o); i.refresh(); Q.notifySuccess(Q.format(Q.text("Site.FavoriteViewsMixin.LoadedViewMessage"), n))
                }
            }); this.populateFavorites()
        } t.prototype.populateFavorites = function () {
            var t = this.getFavorites(); var e = this.ul.children(".save"); this.ul.children().not(e).remove(); var i = Object.keys(t).sort(); if (i.length) {
                for (var n = 0, r = i; n < r.length; n++) {
                    var o = r[n]; var a = $('<li class="fav"><a href="javascript:;"></a></li>').insertBefore(e).children("a").text(o).end(); $('<i class="fa fa-trash-o pull-right"></i>').attr("title", Q.text("Site.FavoriteViewsMixin.DeleteButtonHint")).appendTo(a)
                } $('<li role="separator" class="divider"></li>').insertBefore(e)
            }
        }; t.prototype.saveFavorites = function (t) {
            var e = this.dataGrid.getPersistanceStorage(); var i = "Views:" + this.dataGrid.getPersistanceKey(); e.setItem(i, JSON.stringify(t))
        }; t.prototype.getFavorites = function () {
            var t = this.dataGrid.getPersistanceStorage(); var e = "Views:" + this.dataGrid.getPersistanceKey(); return JSON.parse(t.getItem(e) || "{}") || {}
        }; return t
    }(); t.FavoriteViewsMixin = e
})(Serenity || (Serenity = {}));
var Serenity; (function (t) {
    function e(e) {
        switch (e) {
            case t.SummaryType.Sum: return Slick.Aggregators.Sum; case t.SummaryType.Avg: return Slick.Aggregators.Avg; case t.SummaryType.Max: return Slick.Aggregators.Max; case t.SummaryType.Min: return Slick.Aggregators.Min
        }return null
    } var i = function () {
        function i(i) {
            var n = i.grid; var r = function () {
                var i = n["allColumns"]; var r = []; for (var o = 0, a = i; o < a.length; o++) {
                    var s = a[o]; if (s.sourceItem == null) continue; if (s.sourceItem.summaryType != null && s.sourceItem.summaryType != t.SummaryType.Disabled) {
                        var l = e(s.sourceItem.summaryType); if (l == null) continue; s.summaryType = s.sourceItem.summaryType; r.push(new l(s.field))
                    }
                } return r
            }; n.view.setSummaryOptions({ aggregators: r() }); var o = $('<ul class="s-AggregatesPopup" \n                style="display:none;position:absolute;z-index:100000;">\n                <li data-agg="1">Sum</li>\n                <li data-agg="2">Average</li>\n                <li data-agg="3">Min</li>\n                <li data-agg="4">Max</li>\n            </ul>').appendTo(document.body).click(function (i) {
                if (!$(i.target).is("li")) return; var r = o.data("column"); if (r == null || !r.id) return; var a = Q.tryFirst(n["allColumns"], function (t) {
                    return t.id == r.id
                }); if (!a) return; if (a.sourceItem == null || a.sourceItem.summaryType == null || a.sourceItem.summaryType == t.SummaryType.Disabled) return; var s = Q.toId($(i.target).data("agg")); if (s === a.summaryType) return; a.summaryType = s; var l = n["allColumns"].filter(function (t) {
                    return t.summaryType
                }).map(function (t) {
                    var i = e(t.summaryType); if (i == null) return null; return new i(t.field)
                }).filter(function (t) {
                    return t != null
                }); n.view.setSummaryOptions({ aggregators: l }); var u = n.view.getGrouping(); for (var d = 0; d < u.length; d++)u[d].aggregators = l; n.view.setGrouping(u); n.slickGrid.invalidate()
            }); n.element.on("click", ".slick-footerrow-column", function (e) {
                e.preventDefault(); var i = $(e.currentTarget).data("column"); if (!i || !i.id) return; var r = Q.tryFirst(n["allColumns"], function (t) {
                    return t.id == i.id
                }); if (!r) return; if (!r || !r.sourceItem || r.sourceItem.summaryType == null || r.sourceItem.summaryType == t.SummaryType.Disabled) return; o.data("column", r).show().css("top", e.pageY - o.height() + "px").css("left", e.pageX + "px").children("li").removeClass("active").end().children("li[data-agg=" + r.summaryType + "]").addClass("active"); $(document.body).off("click.aggregatesMenu" + n["uniqueName"]); window.setTimeout(function () {
                    return $(document.body).on("click.aggregatesMenu" + n["uniqueName"], function () {
                        o.hide(); $(document.body).off("click.aggregatesMenu" + n["uniqueName"])
                    })
                }, 1)
            }); var a = n.getCurrentSettings; n.getCurrentSettings = function (t) {
                var e = a.apply(n, [t]); return e
            }; var s = n.restoreSettings; n.restoreSettings = function (t, e) {
                s.apply(n, [t, e])
            }
        } return i
    }(); t.CustomSummaryMixin = i
})(Serenity || (Serenity = {})); var Slick; (function (t) {

    var e; (function (e) {

        var i = function () {

            function e(e) {
                this.onGroupChanged = new t.Event; var i = {}; this.options = $.extend(true, {}, i, e)
            } e.prototype.init = function (t) {
                this.grid = t; this.gridUid = t["getUID"](); this.dropbox = $(t["getGroupingPanel"]()); var e = this.options.dropPlaceHolderText || Q.tryGetText("Site.DraggableGroupingMixin.DropPlaceholder") || "Drag a column header here to group by that column"; this.dropbox.html("<div class='slick-dropped-placeholder'>" + e + "</div>" + ('<div class="slick-group-expand-all" title="' + (Q.tryGetText("Site.DraggableGrupingMixin.ExpandAllButton") || "Expand All") + '" style="display:none"></div>') + ('<div class="slick-group-collapse-all" title="' + (Q.tryGetText("Site.DraggableGrupingMixin.CollapseAllButton") || "Collapse All") + '" style="display:none"></div>')); this.dropboxPlaceholder = this.dropbox.find(".slick-dropped-placeholder"); this.expandAll = this.dropbox.find(".slick-group-expand-all"); this.collapseAll = this.dropbox.find(".slick-group-collapse-all"); this.setupColumnDropbox(); var i = this.getColumns(); for (var n = 0; n < i.length; n++) {
                    var r = i[n]; t["updateColumnHeader"](r.id)
                } this.columnsGroupBy = []
            }; e.prototype.getColumns = function () {
                return this.options.getAllColumns && this.options.getAllColumns() || this.grid.getColumns()
            }; e.prototype.destroy = function () {
                this.onGroupChanged.clear()
            }; e.prototype.setupColumnDropbox = function () {
                var t = this; var e = this; this.dropbox.droppable({
                    activeClass: "ui-state-default", hoverClass: "ui-state-hover", accept: ":not(.ui-sortable-helper)", deactivate: function (e, i) {
                        t.dropbox.removeClass("slick-header-column-denied")
                    }, drop: function (t, i) {
                        e.handleGroupByDrop(this, i.draggable.attr("id").replace(e.gridUid, ""))
                    }, over: function (e, i) {
                        var n = i.draggable.attr("id").replace(t.gridUid, ""); t.getColumns().forEach(function (e, i, r) {
                            if (e.id == n) {
                                if (e.grouping !== false) {
                                    t.dropbox.addClass("slick-header-column-denied")
                                }
                            }
                        })
                    }
                }); this.dropbox.sortable({
                    items: "div.slick-dropped-grouping", cursor: "default", tolerance: "pointer", helper: "clone", update: function (e, i) {
                        var n = $(e.target).sortable("toArray", { attribute: "data-id" }), r = []; for (var o = 0, a = n.length; o < a; o++) {
                            for (var s = 0, l = t.columnsGroupBy.length; s < l; s++) {
                                if (t.columnsGroupBy[s].id == n[o]) {
                                    r.push(t.columnsGroupBy[s]); break
                                }
                            }
                        } t.columnsGroupBy = r; t.updateGroupBy()
                    }
                }); this.expandAll.on("click", function (e) {
                    t.grid.getData().expandAllGroups()
                }); this.collapseAll.on("click", function (e) {
                    t.grid.getData().collapseAllGroups()
                })
            }; e.prototype.getGroupingFor = function (t) {
                var e = null; if (this.options.getGroupingFor) {
                    e = this.options.getGroupingFor(t)
                } if (e === false) return e; return $.extend({
                    getter: t.field || t.id, formatter: function (e) {
                        var i; var n; if (t.formatter) {
                            try { n = t.formatter(-1, -1, e.value, t, (i = {}, i[t.field] = e, i)) } catch (r) {
                                n = Q.htmlEncode(e.value)
                            }
                        } else n = Q.htmlEncode(e.value); return (t.name || t.field || t.id) + ": " + n + " <span style='color:green'>(" + e.count + " items)</span>"
                    }, collapsed: true
                }, e)
            }; e.prototype.handleGroupByDrop = function (t, e) {
                var i = this; var n = true; this.columnsGroupBy.forEach(function (t, i, r) {
                    if (t.id == e) {
                        n = false
                    }
                }); var r = this.getColumns(); if (!n) return; r.forEach(function (n, r, o) {
                    if (n.id !== e || n.grouping === false) return; if (n.grouping == null || $.isEmptyObject(n.grouping)) {
                        n.grouping = i.getGroupingFor(n); if (n.grouping === false) return
                    } var a = $("<div id='" + i.gridUid + n.id + "_entry' data-id='" + n.id + "' class='slick-dropped-grouping'>"); var s = $("<div style='display: inline-flex'>" + n.name + "</div>"); s.appendTo(a); var l = $("<div class='slick-groupby-remove'>&nbsp;</div>"); if (i.options.deleteIconCssClass) l.addClass(i.options.deleteIconCssClass); if (i.options.deleteIconImage) l.css("background", "url(" + i.options.deleteIconImage + ") no-repeat center right"); if (!i.options.deleteIconCssClass && !i.options.deleteIconImage) l.addClass("slick-groupby-remove-image"); l.appendTo(a); $("</div>").appendTo(a); a.appendTo(t); i.addColumnGroupBy(n); l.one("click", function () {
                        return i.removeGroupBy(n.id, a)
                    })
                }); this.grid.invalidate()
            }; e.prototype.addColumnGroupBy = function (t) {
                this.columnsGroupBy.push(t); this.updateGroupBy()
            }; e.prototype.setDroppedGroups = function (t) {
                if (!t || !t.length) {
                    this.columnsGroupBy = []; this.dropbox.find(".slick-dropped-grouping").remove()
                } else {
                    for (var e = 0; e < t.length; e++) {
                        this.handleGroupByDrop(this.dropbox, t[e])
                    }
                } this.updateInterface()
            }; e.prototype.removeGroupBy = function (t, e) {
                e.remove(); var i = this.getColumns(); var n; while ((n = Q.indexOf(this.columnsGroupBy, function (e) {
                    return e.id == t
                })) >= 0) this.columnsGroupBy.splice(n, 1); this.updateGroupBy()
            }; e.prototype.updateInterface = function () {
                var t = !!(this.columnsGroupBy && this.columnsGroupBy.length); this.collapseAll.toggle(t); this.expandAll.toggle(t); this.dropboxPlaceholder.toggle(!t)
            }; e.prototype.updateGroupBy = function () {

                this.updateInterface(); if (this.columnsGroupBy.length == 0) {
                    this.grid.getData().setGrouping([]); this.onGroupChanged.notify({ groupColumns: [] }); this.grid.invalidate(); return
                } var t = []; this.columnsGroupBy.forEach(function (e, i, n) {
                    t.push(e.grouping)
                }); this.grid.getData().setGrouping(t); this.grid.invalidate(); this.onGroupChanged.notify({ groupColumns: t })
            }; e.setupColumnReorder = function (t, e, i, n) {
                e.filter(":ui-sortable").sortable("destroy"); var r = $(t["getGroupingPanel"]()); var o = t.getColumns(); var a = t["getUID"](); e.sortable({
                    distance: 3, cursor: "default", tolerance: "intersection", helper: "clone", placeholder: "slick-sortable-placeholder ui-state-default slick-header-column", forcePlaceholderSize: true, appendTo: "body", start: function (t, e) {
                        $(e.helper).addClass("slick-header-column-active")
                    }, beforeStop: function (t, e) {
                        $(e.helper).removeClass("slick-header-column-active"); var i = r.find(".slick-dropped-grouping").length; if (i > 0) {
                            r.find(".slick-dropped-placeholder").hide(); r.find(".slick-dropped-grouping").show()
                        }
                    }, stop: function (r) {
                        if (!t["getEditorLock"]().commitCurrentEdit()) {
                            $(this).sortable("cancel"); return
                        } var s = e.sortable("toArray"); var l = []; for (var u = 0; u < s.length; u++) {
                            l.push(o[t.getColumnIndex(s[u].replace(a, ""))])
                        } t.setColumns(l); n(t.onColumnsReordered, {}); r.stopPropagation(); i()
                    }
                })
            }; return e
        }(); e.DraggableGrouping = i
    })(e = t.Plugins || (t.Plugins = {}))
})(Slick || (Slick = {}));
var Serenity; (function (t) {
    function e(t, e, i) {
        var n = function (e) {
            return i ? i(e[t]) : e[t]
        }; return function (t, i) {
            var r = n(t), o = n(i); return (r < o ? -1 : r > o ? 1 : 0) * [1, -1][+!!e]
        }
    } var i = function () {
        function t(t) {
            var i = t.grid; var n = this.plugin = new Slick.Plugins.DraggableGrouping({
                getAllColumns: function () {
                    return i.allColumns
                }
            }); i.slickGrid.registerPlugin(n); n.onGroupChanged.subscribe(function () {
                i.persistSettings(null)
            }); var r = i.getCurrentSettings; i.getCurrentSettings = function (t) {
                var e = r.apply(i, [t]); if (t == null || t.groupColumns == null || t.groupColumns) {
                    var n = i.view.getGrouping() || []; e["groupColumns"] = n.map(function (t) {
                        return t.getter
                    }).filter(function (t) {
                        return t != null && typeof t == "string"
                    })
                } return e
            }; function o(t) {
                if (t == null) return; var e = i.allColumns; var r = t.map(function (t) {
                    return Q.tryFirst(e, function (e) {
                        return e.id == t
                    })
                }).filter(function (t) {
                    return t != null
                }); var o = r.map(function (t) {
                    if (t["grouping"] == null) t["grouping"] = n.getGroupingFor(t); return t["grouping"]
                }); i.view.setGrouping(o); n.setDroppedGroups(r.map(function (t) {
                    return t.id
                })); i.slickGrid.invalidate()
            } function a() {
                var t = i.allColumns.filter(function (t) {
                    return t.sourceItem != null && t.sourceItem.groupOrder != null
                }); if (t.length) {
                    return t.sort(e("groupOrder")).map(function (t) {
                        return t.id
                    })
                } return null
            } var s = i.restoreSettings; i.restoreSettings = function (t, e) {
                s.apply(i, [t, e]); if (!(e == null || e.groupColumns == null || e.groupColumns)) return; var n = null; if (t == null) {
                    var r = this.getPersistanceStorage(); if (r != null) {
                        var l = Q.trimToNull(r.getItem(this.getPersistanceKey())); if (l) {
                            n = (JSON.parse(l) || {}).groupColumns
                        }
                    }
                } else n = t.groupColumns; if (n == null) {
                    n = a()
                } o(n)
            }
        } return t
    }(); t.DraggableGroupingMixin = i
})(Serenity || (Serenity = {}));
var Slick; (function (t) {
    var e; (function (e) {
        var i = function () {
            function e(e) {
                this.checkedValues = new window["Set"]; this.onFilterApplied = new t.Event; this.onCommand = new t.Event; this.handler = new t.EventHandler; var i = { buttonImage: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAA4AAAAOCAYAAAAfSC3RAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAALEQAACxEBf2RfkQAAABl0RVh0U29mdHdhcmUAcGFpbnQubmV0IDQuMC4yMfEgaZUAAACeSURBVDhPvdItDsIwGIfxAoZNAQaDmOEgXAIzsRvgCArNCRBcYAmeO+AQHGBuCWqOhYTP521SQsZI2yzhn/xE3+RxVU3WRoSJoz70OpjiiMxijzH0WhhhgzOeP+RIEOA9E6e4oBqdIFGIr0k8xA43mKjADLXR5wY44IEr1rBGZltIWGIuB9f9P1zgDu+whxW8Q1kXS8T65Tn5x6IypV7aOzNezxwrTQAAAABJRU5ErkJggg==", useColumnFormatter: true, getFilterValues: null }; this.options = $.extend(true, {}, i, e); this.handleMenuItemClick = this.handleMenuItemClick.bind(this)
            } e.prototype.init = function (t) {
                this.grid = t; this.handler.subscribe(t.onHeaderCellRendered, this.handleHeaderCellRendered.bind(this)).subscribe(t.onBeforeHeaderCellDestroy, this.handleBeforeHeaderCellDestroy.bind(this)).subscribe(t.onClick, this.handleBodyMouseDown.bind(this)).subscribe(t.onColumnsResized, this.columnsResized.bind(this)); t.setColumns(t.getColumns()); $(document.body).bind("mousedown", this.handleBodyMouseDown = this.handleBodyMouseDown.bind(this))
            }; e.prototype.destroy = function () {
                this.handler.unsubscribeAll(); $(document.body).unbind("mousedown", this.handleBodyMouseDown)
            }; e.prototype.getFilterType = function (t) {
                if (t.headerFilterType != null) return t.headerFilterType; if (this.options.getFilterType != null) {
                    t.headerFilterType = this.options.getFilterType(t); if (t.headerFilterType != null) return t.headerFilterType
                } if (t.headerFilterType == null) t.headerFilterType = 1; return t.headerFilterType
            }; e.prototype.handleBodyMouseDown = function (t) {
                if (this.menu && this.menu[0] != t.target && !$.contains(this.menu[0], t.target)) {
                    this.hideMenu()
                }
            }; e.prototype.hideMenu = function () {
                if (this.menu) {
                    this.menu.remove(); this.menu = null
                }
            }; e.prototype.handleHeaderCellRendered = function (t, e) {
                var i = this; var n = e.column; var r = this.getFilterType(n); if (r === 0) return false; var o = $("<div></div>").addClass("slick-header-menubutton").data("column", n); if (this.options.buttonImage) {
                    o.css("background-image", "url(" + this.options.buttonImage + ")")
                } $(e.node).addClass("has-header-menubutton"); o.on("click", function (t) {
                    t.stopPropagation(); i.showFilter(t)
                }).appendTo(e.node)
            }; e.prototype.handleBeforeHeaderCellDestroy = function (t, e) {
                $(e.node).find(".slick-header-menubutton").remove()
            }; e.prototype.addMenuItem = function (t, e, i, n, r) {
                var o = $("<div class='slick-header-menuitem'>").data("command", n).data("column", e).bind("click", this.handleMenuItemClick).appendTo(t); var a = $("<div class='slick-header-menuicon'>").appendTo(o); if (r) {
                    a.css("background-image", "url(" + r + ")")
                } $("<span class='slick-header-menucontent'>").text(i).appendTo(o)
            }; e.prototype.addSearchInput = function (t, e) {
                var i = this; return $("<input class='input' placeholder='" + (Q.tryGetText("Site.HeaderFiltersMixin.Search") || "Search") + "' style='margin-top: 5px; width: 206px'>").data("column", e).bind("keyup", function (t) {
                    i.updateFilterValues(e, function () {
                    })
                }).appendTo(t)
            }; e.prototype.updateFilterItems = function (t) {
                var e = this; var i = "<label><input type='checkbox' value='-1' />" + (Q.tryGetText("Site.HeaderFiltersMixin.SelectAll") || "(Select All)") + "</label>"; this.checkedValues = new window["Set"](t.headerFilterValues || []); for (var n = 0; n < this.filterValues.length; n++) {
                    var r = this.checkedValues.has(this.filterValues[n]); i += "<label><input type='checkbox' value='" + n + "'" + (r ? " checked='checked'" : "") + "/>" + this.filterTexts[n] + "</label>"
                } var o = this.menu.find(".filter"); o.empty().append($(i)); $(":checkbox", o).bind("click", function (t) {
                    e.onCheckboxClick($(t.target))
                })
            }; e.prototype.getFilterText = function (t, e) {
                var i = t[e.field]; if (this.options.useColumnFormatter && e && e.formatter) {
                    try {
                        var n = e.formatter(-1, -1, i, e, t); if (i == null && n === "") {
                            return Q.tryGetText("Site.HeaderFiltersMixin.Null") || "(null)"
                        } return n
                    } catch (r) {
                    }
                } if (i == null) return Q.tryGetText("Site.HeaderFiltersMixin.Null") || "(null)"; return Q.htmlEncode(i)
            }; e.prototype.showFilter = function (t) {
                var e = this; var i = $(t.target); var n = i.data("column"); this.checkedValues = new window["Set"](n.headerFilterValues || []); if (!this.menu) this.menu = $("<div class='slick-header-menu cke_dialog'>").appendTo(document.body).hide(); else this.menu.empty(); this.searchInput = this.addSearchInput(this.menu, n); var r = "<label><input type='checkbox' value='-1' />" + (Q.tryGetText("Site.HeaderFiltersMixin.SelectAll") || "(Select All)") + "</label>"; var o = $("<div class='filter'>").appendTo(this.menu); $("<button>" + (Q.tryGetText("Site.HeaderFiltersMixin.OkButton") || "OK") + "</button>").appendTo(this.menu).bind("click", function (t) {
                    if (Array["from"]) {
                        n.headerFilterValues = Array["from"](e.checkedValues)
                    } else {
                        n.headerFilterValues = []; e.checkedValues && e.checkedValues.forEach(function (t) {
                            return n.headerFilterValues.push(t)
                        })
                    } e.setButtonImage(i, n.headerFilterValues.length > 0); e.handleApply(t, n)
                }); $("<button>" + (Q.tryGetText("Site.HeaderFiltersMixin.ClearButton") || "Clear") + "</button>").appendTo(this.menu).bind("click", function (t) {
                    if (n.headerFilterValues) n.headerFilterValues.length = 0; e.setButtonImage(i, false); e.handleApply(t, n)
                }); $("<button>" + (Q.tryGetText("Site.HeaderFiltersMixin.CancelButton") || "Cancel") + "</button>").appendTo(this.menu).bind("click", function (t) {
                    return e.hideMenu()
                }); $(":checkbox", o).bind("click", function (t) {
                    e.onCheckboxClick($(t.target))
                }); this.updateFilterValues(n, function () {
                    e.menu.show(); o.css("height", null); var i = e.menu.height(); var n = $(t.target).height(); var r = $(t.target).offset(); var a = r.left - e.menu.width() + $(t.target).width() - 8; var s = r.top + n; var l = $(window).scrollTop(); var u = $(window).height() + l; var d = parseInt($("div.content-wrapper").css("padding-top"), 10) || 0; if (s + i > u && u - s + d < s - l) {
                        s -= i + n + 8; if (s < l) s = l
                    } var c = s + i - u; if (c > 0) {
                        o.css("height", o.height() - c)
                    } e.menu.css("top", s).css("left", a > 0 ? a : 0)
                })
            }; e.prototype.columnsResized = function () {
                this.hideMenu()
            }; e.prototype.onCheckboxClick = function (t) {
                var e = t.val(); var i = t.parent().parent(); if (t.val() < 0) {
                    if (t.prop("checked")) {
                        $(":checkbox", i).prop("checked", true); this.checkedValues = new window["Set"](this.filterValues || [])
                    } else { $(":checkbox", i).prop("checked", false); this.checkedValues.clear() }
                } else {
                    var n = this.filterValues[e]; var r = this.checkedValues.has(n); if (t.prop("checked") && !r) {
                        this.checkedValues.add(n)
                    } else if (r) {
                        this.checkedValues.delete(n)
                    }
                }
            }; e.prototype.setButtonImage = function (t, e) {
                t.toggleClass("is-filtered", e)
            }; e.prototype.handleApply = function (t, e) {
                this.hideMenu(); this.onFilterApplied.notify({ grid: this.grid, column: e }, t, this); t.preventDefault(); t.stopPropagation()
            }; e.prototype.containsFilter = function (t) {
                if (t == null && t.length == 0) return function (t) {
                    return true
                }; var e = Q.htmlEncode(t).toLowerCase(); return function (t) {
                    t = Q.coalesce(t, "").toString().replace(/<[^>]+>/g, "").toLowerCase(); return t.indexOf(e) >= 0
                }
            }; e.prototype.getFilterValue = function (t, e) {
                var i = t[e.field]; if (this.options.useColumnFormatter && e.formatter != null && e.headerFilterType == 2) {
                    return this.getFilterText(t, e).replace(/<[^>]+>/g, "")
                } return i
            }; e.prototype.sortFilterValues = function () {
                var t = this; var e = []; for (var i = 0; i < this.filterValues.length; i++) {
                    e[i] = i
                } e.sort(function (e, i) {
                    var n = t.filterValues[e]; var r = t.filterValues[i]; return n < r ? -1 : n > r ? 1 : 0
                }); var n = []; var r = []; for (var i = 0; i < e.length; i++) {
                    n.push(this.filterValues[i]); r.push(this.filterTexts[i])
                } this.filterValues = n; this.filterTexts = r
            }; e.prototype.updateFilterValues = function (t, e) {
                var i = this; if (this.options.getFilterValues != null) {
                    this.options.getFilterValues(t, function (n, r) {
                        if (n == null) {
                            i.updateFilterValuesFromData(t); i.updateFilterItems(t); e()
                        } else {
                            i.filterValues = []; i.filterTexts = []; r = r || n.map(function (e) {
                                var n = {}; n[t.field] = e; return i.getFilterText(n, t)
                            }); var o = i.containsFilter(i.searchInput.val()); i.filterValues = n.filter(function (t, e) {
                                if (o(r[e])) {
                                    i.filterTexts.push(r[e]); return true
                                } return false
                            }); i.sortFilterValues(); i.updateFilterItems(t); e()
                        }
                    })
                } else { this.updateFilterValuesFromData(t); this.updateFilterItems(t); e() }
            }; e.prototype.updateFilterValuesFromData = function (t) {
                this.filterValues = []; this.filterTexts = []; var e = this.grid.getData(); var i; if (this.checkedValues && this.checkedValues.size) {
                    i = e.getItems()
                } else {
                    i = []; for (var n = 0; n < e.getLength(); n++) {
                        i.push(e.getItem(n))
                    }
                } var r = new window["Set"]; var o = this.searchInput.val(); var a = this.containsFilter(o); for (var s = 0, l = i; s < l.length; s++) {
                    var u = l[s]; var d = this.getFilterValue(u, t); if (!r.has(d)) {
                        var c = this.getFilterText(u, t); if (a(c)) {
                            r.add(d); this.filterValues.push(d); this.filterTexts.push(c)
                        }
                    }
                } this.sortFilterValues()
            }; e.prototype.handleMenuItemClick = function (t) {
                var e = $(this).data("command"); var i = $(this).data("column"); this.hideMenu(); this.onCommand.notify({ grid: this, column: i, command: e }, t, this); t.preventDefault(); t.stopPropagation()
            }; return e
        }(); e.HeaderFilters = i
    })(e = t.Plugins || (t.Plugins = {}))
})(Slick || (Slick = {}));
var Serenity; (function (t) {
    var e = function () {
        function e(e) {
            var i = this; var n = null; var r = {}; if (e.filterByText == null) {
                this.filterByText = !e.grid.view.url || !e.grid.view["getPagingInfo"]().rowsPerPage
            } else this.filterByText = !!e.filterByText; var o = new Slick.Plugins.HeaderFilters({
                getFilterType: function (t) {
                    if (i.filterByText) return 2; if (t.sortable != null && !t.sortable) return 0; if (t.sourceItem != null && t.sourceItem.notFilterable != null && t.sourceItem.notFilterable) return 0; return 1
                }, getFilterValues: function (t, i) {
                    if (t.headerFilterType == 2) return i(null); n = t; try {
                        if (!e.grid.onViewSubmit()) {
                            i([]); return
                        }
                    } finally { n = null } var o = Q.deepClone(e.grid.view.params); o.DistinctFields = [t.field]; o.Skip = 0; o.Take = 0; var a = $.toJSON(o); var s = r[a]; if (s && s.expires > (new Date).getTime()) i(s.value); else {
                        Q.serviceCall({
                            request: o, url: e.grid.view.url, onSuccess: function (t) {
                                r[a] = { value: t.Values, expires: (new Date).getTime() + 1e3 * 30 }; i(t.Values)
                            }
                        })
                    }
                }
            }); o.onFilterApplied.subscribe(function (t, i) {
                var n = i.column; if (n && n.headerFilterType == 2) e.grid.view.setItems(e.grid.view.getItems(), true); else e.grid.refresh()
            }); e.grid.slickGrid.registerPlugin(o); var a = this; var s = e.grid.onViewSubmit; e.grid.onViewSubmit = function () {
                if (!s.call(this)) return false; var e = this.slickGrid.getColumns(); var i = this.view.params; for (var r = 0, o = e; r < o.length; r++) {
                    var a = o[r]; if (a === n || a.headerFilterType == 2) continue; var l = a.headerFilterValues; if (l && l.length) {
                        var u = l.filter(function (t) {
                            return t != null
                        }); var d = [[a.field], "in", [u]]; if (u.length !== l.length) {
                            if (u.length > 0) d = t.Criteria.or(["is null", [a.field]], d); else d = ["is null", [a.field]]
                        } i.Criteria = t.Criteria.and(i.Criteria, d)
                    }
                } return true
            }; var l = e.grid.onViewFilter; e.grid.onViewFilter = function (t) {
                if (!l.apply(this, arguments)) return false; var e = this.slickGrid.getColumns(); for (var i = 0, n = e; i < n.length; i++) {
                    var r = n[i]; if (r.headerFilterType != 2) continue; var a = r.headerFilterValues; if (a && a.length) {
                        var s = o.getFilterValue(t, r); if (a.indexOf(s) < 0) return false
                    }
                } return true
            }
        } return e
    }(); t.HeaderFiltersMixin = e
})(Serenity || (Serenity = {}));
var Serenity; (function (t) {

    var e = function (e) {

        __extends(i, e); function i(t, i) {
            var n = e.call(this, t, i) || this; n.element.on("ondatachange." + n.uniqueName, function () {
                n.subDialogDataChange()
            }); n.element.on("handleroute." + n.uniqueName, function (t, e) {
                if (e.handled) return; if (!e.route && n.isDialogOpen) {
                    n.dialogClose()
                }
            }); $(window).on("resize." + n.uniqueName, function () {
                n.arrange()
            }); return n
        } i.prototype.createSlickGrid = function () {
            var t = e.prototype.createSlickGrid.call(this); this.idPrefix = this.uniqueName + "_DLG_"; this.element.addClass("s-GridWithDialog"); this.dialogPane = $('<div id="' + this.uniqueName + '_DLG" class="s-GridDialogPane flex-layout"></div>').appendTo(this.slickContainer); var i = this.getTemplate().replace(new RegExp("~_", "g"), this.idPrefix); this.dialogPane.html(i); this.initValidator(); this.initTabs(); this.initPropertyGrid(); this.initLocalizationGrid(); return t
        }; i.prototype.destroy = function () {
            $(window).off("resize." + this.uniqueName); if (this.propertyGrid) {
                this.propertyGrid.destroy(); this.propertyGrid = null
            } if (this.localizationGrid) {
                this.localizationGrid.destroy(); this.localizationGrid = null
            } this.undeleteButton = null; this.applyChangesButton = null; this.deleteButton = null; this.saveAndCloseButton = null; this.tabs && this.tabs.tabs("destroy"); this.tabs = null; this.validator && this.byId("Form").remove(); this.validator = null; e.prototype.destroy.call(this)
        }; i.prototype.getItemCssClass = function (t, i) {
            var n = e.prototype.getItemCssClass.call(this, t, i); if (this.isDialogOpen && this.entityId == t[this.getIdProperty()]) {
                return (n || "") + " editing-item"
            } return n
        }; i.prototype.getSaveState = function () {
            try { return $.toJSON(this.getSaveEntity()) } catch (t) {
                return null
            }
        }; i.prototype.hasPendingChanges = function () {
            return this.getSaveState() != this.loadedState
        }; i.prototype.autoSaveOnSwitch = function () {
            return 0
        }; i.prototype.autoSaveOnClose = function () {
            return 0
        }; i.prototype.checkPendingChangesOnSwitch = function (t, e) {
            var i = this; if (this.isDialogOpen && (typeof t == "number" || typeof t == "string")) {
                if (t == this.entityId) return; if (this.autoSaveOnSwitch() && this.hasPendingChanges()) {
                    if (this.autoSaveOnSwitch() == 2 || this.triedAutoSaveForCurrentEntity) {
                        Q.confirm(Q.text("Site.Dialogs.PendingChangesConfirmation"), function () {
                            i.save(function (t) {
                                e()
                            })
                        }, {
                            onNo: function () {
                                i.loadResponse({}); e()
                            }
                        })
                    } else {
                        this.triedAutoSaveForCurrentEntity = true; this.save(function (t) {
                            e()
                        })
                    } return
                }
            } e()
        }; i.prototype.editItem = function (t) {
            var i = this; this.checkPendingChangesOnSwitch(t, function () {
                e.prototype.editItem.call(i, t)
            })
        }; i.prototype.createEntityDialog = function (t, i) {
            if (t == this.getItemType()) {
                i && i(this); return this
            } return e.prototype.createEntityDialog.call(this, t, i)
        }; i.prototype.layout = function () {
            e.prototype.layout.call(this)
        }; i.prototype.byId = function (t) {
            return $("#" + this.idPrefix + t)
        }; i.prototype.getDefaultTemplateName = function () {
            return t.TemplatedDialog.prototype["getDefaultTemplateName"].call(this)
        }; i.prototype.getTemplateName = function () {
            return t.TemplatedDialog.prototype["getTemplateName"].call(this)
        }; i.prototype.getFallbackTemplate = function () {
            return '<div class="s-DialogContent">\n    <div class="s-Form">\n        <form id="~_Form" action="">\n            <div class="fieldset ui-widget ui-widget-content ui-corner-all">\n                <div id="~_PropertyGrid"></div>\n                <div class="clear"></div>\n            </div>\n        </form> \n    </div>\n</div>'
        }; i.prototype.getTemplate = function () {
            return t.TemplatedDialog.prototype["getTemplate"].call(this)
        }; i.prototype.getValidatorOptions = function () {
            return {}
        }; i.prototype.initValidator = function () {
            var t = this.byId("Form"); if (t.length > 0) {
                var e = this.getValidatorOptions(); this.validator = t.validate(Q.validateOptions(e))
            }
        }; i.prototype.resetValidation = function () {
            this.validator && this.validator.resetAll()
        }; i.prototype.validateForm = function () {
            return this.validator == null || !!this.validator.form()
        }; i.prototype.dialogOpen = function () {
            var t = this; if (!this.isDialogOpen) {
                this.element.toggleClass("dialog-open", true); this.isDialogOpen = true; this.updateInterface(); this.layout(); this.onDialogOpen(); Q.Router.dialog(this.element, this.dialogPane, function () {
                    return t.getEditHash()
                }); this.dialogPane.triggerHandler("panelopen")
            }
        }; i.prototype.getEditHash = function () {
            if (this.entityId) return "edit/" + this.entityId.toString(); else return "new"
        }; i.prototype.onDialogOpen = function () {
            if (!$(document.body).hasClass("mobile-device")) $(":input", this.dialogPane).not("button").eq(0).focus(); this.arrange(); this.tabs && this.tabs.tabs("option", "active", 0)
        }; i.prototype.arrange = function () {
            if (window.innerWidth < 768) {
                this.dialogPane.children(".panel-titlebar").prependTo(this.element)
            } else { this.element.children(".panel-titlebar").prependTo(this.dialogPane) } this.dialogPane.find(".require-layout").filter(":visible").each(function (t, e) {
                $(e).triggerHandler("layout")
            })
        }; i.prototype.onDialogClose = function () {
            $(document).trigger("click"); if ($.qtip) {
                $(document.body).children(".qtip").each(function (t, e) {
                    $(e).qtip("hide")
                })
            }
        }; i.prototype.getDialogTitle = function () {
            return ""
        }; i.prototype.dialogClose = function () {
            if (this.isDialogOpen) {
                this.triedAutoSaveForCurrentEntity = false; this.element.toggleClass("dialog-open", false); this.isDialogOpen = false; this.updateInterface(); this.layout(); this.onDialogClose(); this.dialogPane.triggerHandler("panelclose")
            }
        }; Object.defineProperty(i.prototype, "dialogTitle", {
            get: function () {
                return this.element.data("dialogtitle")
            }, set: function (t) {
                var e = this.dialogTitle; this.element.data("dialogtitle", t); if (e != this.dialogTitle) {
                    this.setupDialogTitle(); this.arrange()
                }
            }, enumerable: true, configurable: true
        }); i.prototype.setupDialogTitle = function () {
            var t = this; var e = Q.coalesce(this.dialogTitle, this.getDialogTitle()); var i = this.dialogPane.children(".panel-titlebar").add(this.element.children(".panel-titlebar")); if (Q.isEmptyOrNull(e)) {
                i.remove()
            } else {
                if (!i.length) {
                    i = $("<div class='panel-titlebar'><div class='panel-titlebar-text'></div></div>").prependTo(this.dialogPane)
                } i.children(".panel-titlebar-text").text(e); if (!i.children(".panel-titlebar-close").length) {
                    $('<button class="panel-titlebar-close">&nbsp;</button>').prependTo(i).click(function (e) {
                        if (t.autoSaveOnClose() && t.hasPendingChanges()) {
                            e.preventDefault(); if (t.autoSaveOnClose() == 2 || t.triedAutoSaveForCurrentEntity) {
                                Q.confirm(Q.text("Site.Dialogs.PendingChangesConfirmation"), function () {
                                    return t.saveAndCloseButton.click()
                                }, {
                                    onNo: function () {
                                        t.dialogClose()
                                    }
                                })
                            } else { t.triedAutoSaveForCurrentEntity = true; t.saveAndCloseButton.click() }
                        } else t.dialogClose()
                    })
                }
            }
        }; i.prototype.initTabs = function () {
            var t = this; var e = this.byId("Tabs"); if (e.length === 0) {
                return
            } this.tabs = e.tabs({}); this.tabs.bind("tabsactivate", function () {
                return t.arrange()
            })
        }; i.prototype.get_entity = function () {
            return this.entity
        }; i.prototype.set_entity = function (t) {
            this.entity = t || new Object
        }; i.prototype.get_entityId = function () {
            return this.entityId
        }; i.prototype.set_entityId = function (t) {
            this.entityId = t
        }; i.prototype.getEntityNameFieldValue = function () {
            return Q.coalesce(this.get_entity()[this.getNameProperty()], "").toString()
        }; i.prototype.getEntityTitle = function () {
            if (!this.isEditMode()) {
                return Q.format(Q.text("Controls.EntityDialog.NewRecordTitle"), this.getEntitySingular())
            } else { var t = Q.coalesce(this.getEntityNameFieldValue(), ""); return Q.format(Q.text("Controls.EntityDialog.EditRecordTitle"), this.getEntitySingular(), Q.isEmptyOrNull(t) ? "" : " (" + t + ")") }
        }; i.prototype.updateTitle = function () {
            this.dialogTitle = this.getEntityTitle()
        }; i.prototype.isCloneMode = function () {
            return false
        }; i.prototype.isEditMode = function () {
            return this.get_entityId() != null && !this.isCloneMode()
        }; i.prototype.isDeleted = function () {
            if (this.get_entityId() == null) {
                return false
            } var t = this.getIsDeletedProperty(); if (t) {
                return !!this.get_entity()[t]
            } var e = this.get_entity()[this.getIsActiveProperty()]; if (e == null) {
                return false
            } return e < 0
        }; i.prototype.isNew = function () {
            return this.get_entityId() == null
        }; i.prototype.isNewOrDeleted = function () {
            return this.isNew() || this.isDeleted()
        }; i.prototype.getDeleteOptions = function (t) {
            return {}
        }; i.prototype.deleteHandler = function (t, e) {
            Q.serviceCall(t)
        }; i.prototype.doDelete = function (t) {
            var e = this; var i = this; var n = { EntityId: this.get_entityId() }; var r = {
                service: this.getService() + "/Delete", request: n, onSuccess: function (r) {
                    i.onDeleteSuccess(r); if (t != null) {
                        t(r)
                    } i.element.triggerHandler("ondatachange", [{ entityId: n.EntityId, entity: e.entity, type: "delete" }])
                }
            }; var o = this.getDeleteOptions(t); var a = $.extend(r, o); this.deleteHandler(a, t)
        }; i.prototype.onDeleteSuccess = function (t) {
        }; i.prototype.attrs = function (t) {
            return ss.getAttributes(ss.getInstanceType(this), t, true)
        }; i.prototype.getEntityType = function () {
            var t = e.prototype.getEntityType.call(this); if (Q.endsWith(t, "Grid")) {
                this["entityType"] = t = t.substr(0, t.length - 4)
            } return t
        }; i.prototype.getFormKey = function () {
            if (this.formKey != null) return this.formKey; var e = this.attrs(t.FormKeyAttribute); if (e.length >= 1) return this.formKey = e[0].value; return this.formKey = this.getEntityType()
        }; i.prototype.getEntitySingular = function () {
            if (this.entitySingular != null) return this.entitySingular; var e = this.attrs(t.ItemNameAttribute); if (e.length >= 1) {
                this.entitySingular = e[0].value; this.entitySingular = Q.LT.getDefault(this.entitySingular, this.entitySingular)
            } else { var i = Q.tryGetText(this.getLocalTextDbPrefix() + "EntitySingular"); if (i == null) i = this.getEntityType(); this.entitySingular = i } return this.entitySingular
        }; i.prototype.getNameProperty = function () {
            if (this.nameProperty != null) return this.nameProperty; var e = this.attrs(t.NamePropertyAttribute); if (e.length >= 1) this.nameProperty = e[0].value; else this.nameProperty = "Name"; return this.nameProperty
        }; i.prototype.getIsDeletedProperty = function () {
            return null
        }; i.prototype.load = function (t, e, i) {
            var n = this; var r = function () {
                if (t == null) {
                    n.loadResponse({}); e && e(); return
                } var i = typeof t; if (i === "string" || i === "number") {
                    var r = t; n.loadById(r, function (t) {
                        if (e) window.setTimeout(e, 0)
                    }, null); return
                } var o = t || new Object; n.loadResponse({ Entity: o }); e && e()
            }; if (i == null) {
                r(); return
            } try { r() } catch (o) {
                var a = ss.Exception.wrap(o); i(a)
            }
        }; i.prototype.loadNewAndOpenDialog = function () {
            this.loadResponse({}); this.dialogOpen()
        }; i.prototype.loadEntityAndOpenDialog = function (t, e) {
            this.loadResponse({ Entity: t }); this.dialogOpen()
        }; i.prototype.loadResponse = function (t) {
            var e = this.entityId; t = t || {}; this.onLoadingData(t); var i = t.Entity || new Object; this.beforeLoadEntity(i); this.loadEntity(i); this.set_entity(i); this.afterLoadEntity(); if (this.isDialogOpen && e != this.entityId) Q.Router.replaceLast(this.getEditHash(), true); if (this.autoSaveOnClose() || this.autoSaveOnSwitch()) {
                this.triedAutoSaveForCurrentEntity = false; this.loadedState = this.getSaveState()
            }
        }; i.prototype.loadEntity = function (t) {
            var e = this.getIdProperty(); if (e != null) this.set_entityId(t[e]); this.set_entity(t); if (this.propertyGrid != null) {
                this.propertyGrid.set_mode(this.isEditMode() ? 2 : 1); this.propertyGrid.load(t)
            }
        }; i.prototype.beforeLoadEntity = function (t) {
            this.localizationPendingValue = null; this.localizationLastValue = null
        }; i.prototype.afterLoadEntity = function () {
            this.updateInterface(); this.updateTitle(); this.slickGrid.invalidate()
        }; i.prototype.loadByIdAndOpenDialog = function (t) {
            var e = this; this.loadById(t, function (t) {
                return window.setTimeout(function () {
                    return e.dialogOpen()
                }, 0)
            }, function () {
                if (!e.element.is(":visible")) {
                    e.element.remove()
                }
            })
        }; i.prototype.onLoadingData = function (t) {
        }; i.prototype.getLoadByIdOptions = function (t, e) {
            return {}
        }; i.prototype.getLoadByIdRequest = function (t) {
            var e = {}; e.EntityId = t; return e
        }; i.prototype.reloadById = function () {
            this.loadById(this.get_entityId())
        }; i.prototype.loadById = function (t, e, i) {
            var n = this; var r = {
                service: this.getService() + "/Retrieve", blockUI: true, request: this.getLoadByIdRequest(t), onSuccess: function (t) {
                    n.loadResponse(t); e && e(t)
                }, onCleanup: function () {
                    if (n.validator != null) {
                        Q.validatorAbortHandler(n.validator)
                    }
                }
            }; var o = this.getLoadByIdOptions(t, e); var a = $.extend(r, o); this.loadByIdHandler(a, e, i)
        }; i.prototype.loadByIdHandler = function (t, e, i) {
            var n = Q.serviceCall(t); i && n.fail(i)
        }; i.prototype.initLocalizationGrid = function () {
            var t = this.byId("PropertyGrid"); if (t.length <= 0) {
                return
            } var e = this.getPropertyGridOptions(); this.initLocalizationGridCommon(e)
        }; i.prototype.initLocalizationGridCommon = function (e) {
            var i = this.byId("PropertyGrid"); if (!Q.any(e.items, function (t) {
                return t.localizable === true
            })) return; var n = $("<div/>").attr("id", this.idPrefix + "LocalizationGrid").hide().insertAfter(i); e.idPrefix = this.idPrefix + "Localization_"; var r = []; for (var o = 0, a = e.items; o < a.length; o++) {
                var s = a[o]; var l = null; if (s.localizable === true) {
                    var u = $.extend({}, s); u.oneWay = true; u.readOnly = true; u.required = false; u.defaultValue = null; r.push(u); if (l == null) l = this.getLangs(); for (var d = 0, c = l; d < c.length; d++) {
                        var p = c[d]; u = $.extend({}, s); u.name = p[0] + "$" + u.name; u.title = p[1]; u.cssClass = [u.cssClass, "translation"].join(" "); u.insertable = true; u.updatable = true; u.oneWay = false; u.required = false; u.localizable = false; u.defaultValue = null; r.push(u)
                    }
                }
            } e.items = r; this.localizationGrid = new t.PropertyGrid(n, e).init(null); n.addClass("s-LocalizationGrid")
        }; i.prototype.isLocalizationMode = function () {
            return this.localizationButton != null && this.localizationButton.hasClass("pressed")
        }; i.prototype.isLocalizationModeAndChanged = function () {
            if (!this.isLocalizationMode()) {
                return false
            } var t = this.getLocalizationGridValue(); return $.toJSON(this.localizationLastValue) != $.toJSON(t)
        }; i.prototype.localizationButtonClick = function () {
            if (this.isLocalizationMode() && !this.validateForm()) {
                return
            } if (this.isLocalizationModeAndChanged()) {
                var t = this.getLocalizationGridValue(); this.localizationLastValue = t; this.localizationPendingValue = t
            } this.localizationButton.toggleClass("pressed"); this.updateInterface(); if (this.isLocalizationMode()) {
                this.loadLocalization()
            }
        }; i.prototype.getLanguages = function () {
            if (t.EntityDialog.defaultLanguageList != null) return t.EntityDialog.defaultLanguageList() || []; return []
        }; i.prototype.getLangs = function () {
            var t = this.getLanguages(); var e = ss.safeCast(t, Array); if (e == null || e.length === 0 || e[0] == null || !Q.isArray(e[0])) {
                e = Array.prototype.slice.call(t.map(function (t) {
                    return [t.item1, t.item2]
                }))
            } return e
        }; i.prototype.loadLocalization = function () {
            var t = this; if (this.localizationLastValue == null && this.isNew()) {
                this.localizationGrid.load({}); this.setLocalizationGridCurrentValues(); this.localizationLastValue = this.getLocalizationGridValue(); return
            } if (this.localizationLastValue != null) {
                this.localizationGrid.load(this.localizationLastValue); this.setLocalizationGridCurrentValues(); return
            } var e = {
                service: this.getService() + "/Retrieve", blockUI: true, request: { EntityId: this.get_entityId(), ColumnSelection: "keyOnly", IncludeColumns: ["Localizations"] }, onSuccess: function (e) {
                    var i = $.extend(new Object, t.get_entity()); if (e.Localizations) {
                        for (var n = 0, r = Object.keys(e.Localizations); n < r.length; n++) {
                            var o = r[n]; var a = e.Localizations[o]; for (var s = 0, l = Object.keys(a); s < l.length; s++) {
                                var u = l[s]; i[o + "$" + u] = a[u]
                            }
                        }
                    } t.localizationGrid.load(i); t.setLocalizationGridCurrentValues(); t.localizationPendingValue = null; t.localizationLastValue = t.getLocalizationGridValue()
                }
            }; Q.serviceCall(e)
        }; i.prototype.setLocalizationGridCurrentValues = function () {
            var t = this; var e = {}; this.localizationGrid.enumerateItems(function (i, n) {
                if (i.name.indexOf("$") < 0 && n.element.is(":input")) {
                    e[i.name] = t.byId(i.name).val(); n.element.val(e[i.name])
                }
            }); this.localizationGrid.enumerateItems(function (t, i) {
                var n = t.name.indexOf("$"); if (n >= 0 && i.element.is(":input")) {
                    var r = e[t.name.substr(n + 1)]; if (r != null && r.length > 0) {
                        i.element.attr("title", r).attr("placeholder", r)
                    }
                }
            })
        }; i.prototype.getLocalizationGridValue = function () {
            var t = {}; this.localizationGrid.save(t); for (var e = 0, i = Object.keys(t); e < i.length; e++) {
                var n = i[e]; if (n.indexOf("$") < 0) {
                    delete t[n]
                }
            } return t
        }; i.prototype.getPendingLocalizations = function () {
            if (this.localizationPendingValue == null) {
                return null
            } var t = {}; var e = this.getIdProperty(); var i = this.getLangs(); for (var n = 0, r = i; n < r.length; n++) {
                var o = r[n]; var a = o[0]; var s = {}; if (e != null) {
                    s[e] = this.get_entityId()
                } var l = a + "$"; for (var u = 0, d = Object.keys(this.localizationPendingValue); u < d.length; u++) {
                    var c = d[u]; if (Q.startsWith(c, l)) s[c.substr(l.length)] = this.localizationPendingValue[c]
                } t[a] = s
            } return t
        }; i.prototype.initPropertyGrid = function () {
            var e = this.byId("PropertyGrid"); if (e.length <= 0) {
                return
            } var i = this.getPropertyGridOptions(); this.propertyGrid = new t.PropertyGrid(e, i).init(null); if (this.element.closest(".ui-dialog").hasClass("s-Flexify")) {
                this.propertyGrid.element.children(".categories").flexHeightOnly(1)
            }
        }; i.prototype.getFormPropertyItems = function () {
            var t = this.getFormKey(); return Q.getForm(t)
        }; i.prototype.getPropertyGridOptions = function () {
            return { idPrefix: this.idPrefix, items: this.getFormPropertyItems(), mode: 1, localTextPrefix: "Forms." + this.getFormKey() + ".", useCategories: true }
        }; i.prototype.validateBeforeSave = function () {
            return true
        }; i.prototype.getSaveOptions = function (t) {
            var e = this; var i = {}; i.service = this.getService() + "/" + (this.isEditMode() ? "Update" : "Create"), i.onSuccess = function (n) {
                e.triedAutoSaveForCurrentEntity = false; e.onSaveSuccess(n); t && t(n); var r = e.isEditMode() ? "update" : "create"; var o = i.request == null ? null : i.request.Entity; var a = e.isEditMode() ? e.get_entityId() : n == null ? null : n.EntityId; var s = { type: r, entity: o, entityId: a }; e.element.triggerHandler("ondatachange", [s])
            }; i.onCleanup = function () {
                e.validator && Q.validatorAbortHandler(e.validator)
            }; i.request = this.getSaveRequest(); return i
        }; i.prototype.getSaveEntity = function () {
            var t = new Object; if (this.propertyGrid != null) {
                this.propertyGrid.save(t)
            } if (this.isEditMode()) {
                var e = this.getIdProperty(); if (e != null && t[e] == null) {
                    t[e] = this.get_entityId()
                }
            } return t
        }; i.prototype.getSaveRequest = function () {
            var t = this.getSaveEntity(); var e = {}; e.Entity = t; if (this.isEditMode()) {
                var i = this.getIdProperty(); if (i != null) {
                    e.EntityId = this.get_entityId()
                }
            } if (this.localizationPendingValue != null) {
                e.Localizations = this.getPendingLocalizations()
            } return e
        }; i.prototype.onSaveSuccess = function (t) {
        }; i.prototype.save_submitHandler = function (t) {
            var e = this.getSaveOptions(t); this.saveHandler(e, t)
        }; i.prototype.save = function (e) {
            var i = this; return t.ValidationHelper.submit(this.byId("Form"), function () {
                return i.validateBeforeSave()
            }, function () {
                return i.save_submitHandler(e)
            })
        }; i.prototype.saveHandler = function (t, e) {
            Q.serviceCall(t)
        }; i.prototype.createToolbar = function (t) {

            e.prototype.createToolbar.call(this, t); if (!this.toolbar) return; this.saveAndCloseButton = this.toolbar.findButton("save-and-close-button").hide(); this.saveAndCloseButton.prev(".separator").addClass("dialog-button-separator"); this.applyChangesButton = this.toolbar.findButton("apply-changes-button").hide(); this.deleteButton = this.toolbar.findButton("delete-button").hide(); this.undeleteButton = this.toolbar.findButton("undo-delete-button").hide(); this.cloneButton = this.toolbar.findButton("clone-button").hide();
            this.localizationButton = this.toolbar.findButton("localization-button").hide()
        }; i.prototype.showSaveSuccessMessage = function (t) {
            Q.notifySuccess(Q.text("Controls.EntityDialog.SaveSuccessMessage"), "", null)
        }; i.prototype.getButtons = function () {
            var i = this; var n = e.prototype.getButtons.call(this); n.push({
                title: Q.text("Controls.EntityDialog.SaveButton"), cssClass: "save-and-close-button dialog-button", hotkey: "alt+s", separator: true, onClick: function () {
                    i.save(function (t) {
                        i.dialogClose()
                    })
                }
            }); n.push({
                title: "", hint: Q.text("Controls.EntityDialog.ApplyChangesButton"), cssClass: "apply-changes-button dialog-button", hotkey: "alt+a", onClick: function () {
                    i.save(function (t) {
                        if (i.isEditMode()) {
                            var e = t.EntityId; if (e == null) {
                                e = i.get_entityId()
                            } i.loadById(e)
                        } else { i.loadById(t.EntityId) } i.showSaveSuccessMessage(t)
                    })
                }
            }); n.push({
                title: Q.text("Controls.EntityDialog.DeleteButton"), cssClass: "delete-button dialog-button", hotkey: "alt+x", onClick: function () {
                    Q.confirm(Q.text("Controls.EntityDialog.DeleteConfirmation"), function () {
                        i.doDelete(function () {
                            return i.dialogClose()
                        })
                    })
                }
            }); n.push({
                title: Q.text("Controls.EntityDialog.UndeleteButton"), cssClass: "undo-delete-button dialog-button", onClick: function () {
                    if (i.isDeleted()) {
                        Q.confirm(Q.text("Controls.EntityDialog.UndeleteConfirmation"), function () {
                            i.undelete(function () {
                                return i.loadById(i.get_entityId())
                            })
                        })
                    }
                }
            }); n.push({
                title: Q.text("Controls.EntityDialog.LocalizationButton"), cssClass: "localization-button dialog-button", onClick: function () {
                    return i.localizationButtonClick()
                }
            }); n.push({
                title: Q.text("Controls.EntityDialog.CloneButton"), cssClass: "clone-button dialog-button", onClick: function () {
                    if (!i.isEditMode()) {
                        return
                    } var e = i.getCloningEntity(); t.Widget.create({
                        type: ss.getInstanceType(i), init: function (n) {
                            return t.SubDialogHelper.bubbleDataChange(t.SubDialogHelper.cascade(n, i.element), i, true).loadEntityAndOpenDialog(e, null)
                        }
                    })
                }
            }); return n
        }; i.prototype.getCloningEntity = function () {
            var t = new Object; t = $.extend(t, this.get_entity()); var e = this.getIdProperty(); if (!Q.isEmptyOrNull(e)) {
                delete t[e]
            } var i = this.getIsActiveProperty(); if (!Q.isEmptyOrNull(i)) {
                delete t[i]
            } var n = this.getIsDeletedProperty(); if (!Q.isEmptyOrNull(n)) {
                delete t[n]
            } return t
        }; i.prototype.updateInterface = function () {
            var e = this.isDialogOpen; var i = this.isDeleted(); var n = this.isLocalizationMode(); if (this.tabs != null) {
                t.TabsExtensions.setDisabled(this.tabs, "Log", !e || this.isNewOrDeleted())
            } if (this.propertyGrid != null) {
                this.propertyGrid.element.toggle(e && !n)
            } if (this.localizationGrid != null) {
                this.localizationGrid.element.toggle(e && n)
            } if (this.localizationButton != null) {
                this.localizationButton.toggle(this.localizationGrid != null); this.localizationButton.find(".button-inner").text(this.isLocalizationMode() ? Q.text("Controls.EntityDialog.LocalizationBack") : Q.text("Controls.EntityDialog.LocalizationButton"))
            } if (n && e) {
                if (this.toolbar != null) this.toolbar.findButton("tool-button").not(".localization-hidden").addClass(".localization-hidden").hide(); this.localizationButton && this.localizationButton.show(); return
            } this.toolbar.findButton("localization-hidden").removeClass("localization-hidden").show(); this.deleteButton && this.deleteButton.toggle(e && this.isEditMode() && !i); this.undeleteButton && this.undeleteButton.toggle(e && this.isEditMode() && i); if (this.saveAndCloseButton) {
                this.saveAndCloseButton.toggle(e && !i); this.saveAndCloseButton.find(".button-inner").text(Q.text(this.isNew() ? "Controls.EntityDialog.SaveButton" : "Controls.EntityDialog.UpdateButton"))
            } this.applyChangesButton && this.applyChangesButton.toggle(e && !i); this.cloneButton && this.cloneButton.toggle(false)
        }; i.prototype.getUndeleteOptions = function (t) {
            return {}
        }; i.prototype.undeleteHandler = function (t, e) {
            Q.serviceCall(t)
        }; i.prototype.undelete = function (t) {
            var e = this; var i = {}; i.service = this.getService() + "/Undelete"; var n = {}; n.EntityId = this.get_entityId(); i.request = n; i.onSuccess = function (i) {
                t && t(i); e.element.triggerHandler("ondatachange", [{ entityId: e.get_entityId(), entity: e.entity, type: "undelete" }])
            }; var r = this.getUndeleteOptions(t); var o = $.extend(i, r); this.undeleteHandler(o, t)
        }; i = __decorate([t.Decorators.registerClass("Serenity.EntityGridDialog", [t["IEditDialog"]])], i); return i
    }(t.EntityGrid); t.EntityGridDialog = e
})(Serenity || (Serenity = {}));

;