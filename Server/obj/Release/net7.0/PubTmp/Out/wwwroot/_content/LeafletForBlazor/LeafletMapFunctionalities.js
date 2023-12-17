import { tooltip_object, is_json_object, _is_point, _is_polygon, _is_linestring } from "./helper.js"; import { _init_basemap_layers } from "./basemap_layers.js"; import { _init_esri_basemap_layers, _init_geocoding_search_, _init_reverse_geocoding_ } from "./esri_interops.js"; import { init_geolet_plugins, init_linear_measurement } from "./plugins.js"; import { _init_toggle_button } from "./custom_map_controls.js"; import { _image_overlay_, _video_overlay_ } from "./other_overlays.js"; let map = null, leaflet_basemap_layers_initialized = !1, layers_control = null, _parameters_for_all_plugins_ = null, _parameters_for_esri_plugins_ = null, _parameters_for_ui_controls_ = null, _parameters_for_images_overlay = null, _esri_basemap_layers_exist_in_plugin_config_ = !1, _L = null; export function load_leafletmap_core(t, e) { console.info("%cLeafletForBlazor parameters initialization...", "color:#0381FF;font-weight:bold;font-size: 16px;"); let o = JSON.parse(e), l = o.load_parameters.location.latitude, a = o.load_parameters.location.longitude, n = o.load_parameters.zoom_level, i = o.load_parameters.map_scale, r = o.load_parameters.basemap, s = o.load_parameters.anyway_overlay_layers_control, p = o.map_options.interaction_options.doubleClickZoom, d = o.map_options.interaction_options.shiftBoxZoom, c = o.map_options.interaction_options.dragging, u = o.map_options.interaction_options.trackResize, m = o.map_options.interaction_options.keyboard, f = o.map_options.interaction_options.keyboardPanDelta, g = null !== o._geojson ? o._geojson : [], y = [], h = [], $ = [], v = [], b = 0; if (void 0 !== o._data_versions && null !== o._data_versions && 0 !== o._data_versions.length) for (let z of g) { let T = o._data_versions[b], _ = o._has_tooltip[b], k = o.geojson_file_name[b]; is_json_object(z) && (0 === T ? (y.push(JSON.parse(z)), $.push(_), v.push(k)) : 1 === T && (h.push(JSON.parse(z)), $.push(_), v.push(k))), b++ } let w = document.createElement("script"); w.src = "https://unpkg.com/leaflet@1.9.3/dist/leaflet.js", w.async = !0; let x = document.createElement("link"); return x.href = "https://unpkg.com/leaflet@1.9.3/dist/leaflet.css", x.rel = "stylesheet", w.onload = () => { console.info("%cLeafletForBlazor map initialization...", "color:#0381FF;font-weight:bold;font-size: 16px;"), _load_leaflet_map(l, a, n, i, p, d, c, u, m, f, s, 0 !== y.length || 0 !== h.length, t), leaflet_basemap_layers_initialized = _init_basemap_layers(map, r, layers_control, _esri_basemap_layers_exist_in_plugin_config_), _init_esri_plugins_(), _plugins_initialization_(map, L), (y.length >= 0 || h.length >= 0) && _geojson_data_initialization(y, h, $, v) }, w.onerror = () => { console.warn("Error occurred while loading Leaflet script") }, document.body.appendChild(x), document.body.appendChild(w), "" } export function stream_points_update(t, e) { let o = document.createElement("script"); o.src = "https://unpkg.com/leaflet@1.9.3/dist/leaflet.js", o.async = !0; let l = document.createElement("link"); l.href = "https://unpkg.com/leaflet@1.9.3/dist/leaflet.css", l.rel = "stylesheet"; let a = JSON.parse(e); o.onload = () => { console.log(a), L.geoJSON(a).addTo(map) }, document.body.appendChild(l), document.body.appendChild(o) } export function plugins_interop(t, e) { _parameters_for_all_plugins_ = JSON.parse(e) } export function images_overlay(t, e) { _parameters_for_images_overlay = JSON.parse(e) } export function init_esri(t, e) { let o = !1, l = JSON.parse(_parameters_for_esri_plugins_ = e); if (null !== l && void 0 !== l.esri_plugins_config && null !== l.esri_plugins_config) { let a = l.esri_plugins_config.map(function (t, e) { return [Object.keys(t), e] }); for (let n of a) o = "esri_basemap_layers" === n[0][1] && l.esri_plugins_config[n[1]].enable } _esri_basemap_layers_exist_in_plugin_config_ = o } export function create_ui_controls(t, e) { _parameters_for_ui_controls_ = JSON.parse(e) } export function _plugins_initialization_(map, L) { if (void 0 !== _parameters_for_all_plugins_.display_all && _parameters_for_all_plugins_.display_all) { for (let item of (console.info("%cLeafletForBlazor plugins initialization:", "color:#00ff00;font-weight:bold;font-size: 16px;"), _parameters_for_all_plugins_.plugins)) if ("Geolet" === item.name) { if (console.info(`%c - plugin ${item.name} initialization...`, "color:#008000;font-weight:normal;font-size: 14px;font-style:italic;"), init_geolet_plugins(), !item.config) { console.error("Geolet plugin...config parameter is not configured"); return } let config = JSON.parse(item.config); L.geolet({ position: void 0 === config.position ? "topleft" : config.position, title: void 0 === config.title ? "title" : config.title, popupContent: function (latlng) { let config_popup = {}; return !1 == !config.popupContent ? eval(config.popupContent) : `${latlng.lat} / ${latlng.lng}` } }).addTo(map) } } } export function _init_esri_plugins_() { if (null != _parameters_for_esri_plugins_) { let t = document.createElement("script"); t.src = "https://unpkg.com/esri-leaflet@3.0.10/dist/esri-leaflet.js", t.async = !0; let e = document.createElement("script"); e.src = "https://unpkg.com/esri-leaflet-vector@4.0.0/dist/esri-leaflet-vector.js", e.async = !0, t.onload = () => { e.onload = () => { let t = JSON.parse(_parameters_for_esri_plugins_); if (null != t && !0 === Array.isArray(t)) for (let e of t) null != e && void 0 !== e.enable && null !== e.enable && e.enable && void 0 !== e.type && null !== e.type && "EsriBasemapConfig" === e.type && (console.info(`%c - plugin ${e.type} initialization...`, "color:#008000;font-weight:normal;font-size: 14px;font-style:italic;"), _init_esri_basemap_layers(map, e.esri_basemap_layers, e.apiKey, layers_control, leaflet_basemap_layers_initialized)), null != e && void 0 !== e.enable && null !== e.enable && e.enable && void 0 !== e.type && null !== e.type && "EsriGeocodingSearchParameters" === e.type && (console.info(`%c - plugin ${e.type} initialization...`, "color:#008000;font-weight:normal;font-size: 14px;font-style:italic;"), _init_geocoding_search_(map, e)), null != e && void 0 !== e.enable && null !== e.enable && e.enable && void 0 !== e.type && null !== e.type && "EsriReverseGeocodingParameters" === e.type && (console.info(`%c - plugin ${e.type} initialization...`, "color:#008000;font-weight:normal;font-size: 14px;font-style:italic;"), _init_reverse_geocoding_(map, e)) } }, document.body.appendChild(t), document.body.appendChild(e) } } export function _load_leaflet_map(t, e, o, l, a, n, i, r, s, p, d, c, u) { for (let m of (console.info("%cLeafletForBlazor geojson data initialization...", "color:#0381FF;font-weight:bold;font-size: 16px;"), (map = L.map("map", { doubleClickZoom: a, shiftBoxZoom: n, dragging: i, trackResize: r, keyboard: s, keyboardPanDelta: p }).on("load", function (l) { let a = l.target.getBounds(); u.invokeMethodAsync("LoadMapPartial", { location: { longitude: t, latitude: e }, zoom_level: o, map_bounds: { _southWest: { latitude: a._southWest.lat, longitude: a._southWest.lng }, _northEast: { latitude: a._northEast.lat, longitude: a._northEast.lng } } }) }).setView({ lon: e, lat: t }, o)).on("click", function (t) { let e = t.target.getBounds(); t.target.getCenter(), u.invokeMethodAsync("OnClickMap", { location: { longitude: t.latlng.lng, latitude: t.latlng.lat }, zoom_level: void 0 === t.sourceTarget._zoom ? -1 : t.sourceTarget._zoom, map_bounds: { _southWest: { latitude: e._southWest.lat, longitude: e._southWest.lng }, _northEast: { latitude: e._northEast.lat, longitude: e._northEast.lng } } }) }), map.on("dblclick", function (t) { let e = t.target.getBounds(); t.target.getCenter(), u.invokeMethodAsync("OnDblClickMap", { location: { longitude: t.latlng.lng, latitude: t.latlng.lat }, zoom_level: void 0 === t.sourceTarget._zoom ? -1 : t.sourceTarget._zoom, map_bounds: { _southWest: { latitude: e._southWest.lat, longitude: e._southWest.lng }, _northEast: { latitude: e._northEast.lat, longitude: e._northEast.lng } } }) }), map.on("zoom", function (t) { let e = t.target.getBounds(), o = t.target.getCenter(); u.invokeMethodAsync("OnZoomChange", { location: { longitude: o.lng, latitude: o.lat }, zoom_level: t.sourceTarget._zoom, map_bounds: { _southWest: { latitude: e._southWest.lat, longitude: e._southWest.lng }, _northEast: { latitude: e._northEast.lat, longitude: e._northEast.lng } } }) }), map.on("move", function (t) { let e = t.target.getBounds(), o = t.target.getCenter(); u.invokeMethodAsync("OnMoveChange", { location: { longitude: o.lng, latitude: o.lat }, zoom_level: void 0 === t.sourceTarget._zoom ? -1 : t.sourceTarget._zoom, map_bounds: { _southWest: { latitude: e._southWest.lat, longitude: e._southWest.lng }, _northEast: { latitude: e._northEast.lat, longitude: e._northEast.lng } } }) }), void 0 !== l.has && !0 === l.has && L.control.scale({ metric: l.meters, imperial: l.miles }).addTo(map), !0 === d && c && _layers_control(map), _parameters_for_ui_controls_.collection)) "UIToggleButton" === m.type && _init_toggle_button(m.style.disable_color, m.style.enable_color, m.style.tick_button_color, m.position, m.name, m.title, L, map, function (t) { u.invokeMethodAsync("OnToggleSwitch", t) }); if (null != _parameters_for_images_overlay && 0 !== _parameters_for_images_overlay.length) { let f = []; for (let g of _parameters_for_images_overlay) { let y = g.url, h = [[g.map_bounds._southWest.latitude, g.map_bounds._southWest.longitude], [g.map_bounds._northEast.latitude, g.map_bounds._northEast.longitude]], $ = { url: y, bound: h }; f.push($) } _image_overlay_(L, map, f) } } export function _layers_control(t) { layers_control = L.control.layers(null, null, { collapsed: !0 }).addTo(t) } export function _geojson_data_initialization(t, e, o, l) { if (null != t && 0 !== t.length) { let a = 0; for (let n of t) _add_without_any_symbols(n, null, l[a]), a++ } if (null != e && 0 !== e.length) { let i = 0; for (let r of e) { if (void 0 !== r.data) { let s = o[i], p = r.symbology, d = r.data, c = _is_point(d); if (void 0 !== p && void 0 !== p.case) { if (!1 === c) { let u = L.geoJSON(d, { style: function (t) { if (s) { if (_is_polygon(d)) { var e = tooltip_object(r.tooltip, t); let o = L.polygon(e.coordinates, { opacity: e.opacity }).bindTooltip(e.content, { permanent: e.permanent }).addTo(map); _tooltips_scaling(e, o) } else if (_is_linestring(d)) { var e = tooltip_object(r.tooltip, t); let l = L.polyline(e.coordinates, { opacity: e.opacity }).bindTooltip(e.content, { permanent: e.permanent }).addTo(map); _tooltips_scaling(e, l) } } return _is_polygon(d) ? _get_symbol(p, t) : _is_linestring(d) ? _get_symbol(p, t.geometry) : void 0 } }).addTo(map); _layer_scaling(p, u), null !== layers_control && layers_control.addOverlay(u, l[i]) } else _add_case_symbols_and_tooltip_point(r, o[i], l[i]) } else if (void 0 !== p) { if (!1 === c) { if (_is_polygon(d)) { let m = L.geoJSON(d, { style: function (t) { if (s) { var e = tooltip_object(r.tooltip, t); let o = L.polygon(e.coordinates, { opacity: e.opacity }).bindTooltip(e.content, { permanent: e.permanent }).addTo(map); _tooltips_scaling(e, o) } return p.default } }).addTo(map); _layer_scaling(p, m), null !== layers_control && layers_control.addOverlay(m, l[i]) } if (_is_linestring(d)) { let f = L.geoJSON(d, { style: function (t) { if (s) { var e = tooltip_object(r.tooltip, t); let o = L.polyline(e.coordinates, { opacity: e.opacity }).bindTooltip(e.content, { permanent: e.permanent }).addTo(map); _tooltips_scaling(e, o) } return p.default } }).addTo(map); _layer_scaling(p, f), null !== layers_control && layers_control.addOverlay(f, l[i]) } } else { let g = L.geoJSON(d, { pointToLayer: function (t, e) { if (s) { let o = tooltip_object(r.tooltip, t); var l = new L.marker(o.coordinates, { opacity: 0 }); let a = l.bindTooltip(o.content, { permanent: o.permanent, className: "leafletforblazor-tooltip", offset: o.offset }); l.addTo(map), _tooltips_scaling(o, a) } return L.circleMarker(e, p.default) } }).addTo(map); _layer_scaling(p, g), null !== layers_control && layers_control.addOverlay(g, l[i]) } } else void 0 !== r.data && _add_without_any_symbols(r.data, r.tooltip, l[i]) } else console.warn("Content of JSON file is not valid! Symbology Version"); i++ } } else console.warn("Content of JSON file is not valid!") } export function _get_symbol(t, e) { return void 0 !== t.case.classes.find(o => o.value === e.properties[t.case.field_name]) ? t.case.classes.find(o => o.value === e.properties[t.case.field_name]).symbol : t.default } export function _add_case_symbols_and_tooltip_point(t, e, o) { let l = t.symbology, a = t.data, n = L.geoJSON(a, { pointToLayer: function (o, a) { if (e) { let n = tooltip_object(t.tooltip, o); var i = new L.marker(n.coordinates, { opacity: 0 }); let r = i.bindTooltip(n.content, { permanent: n.permanent, className: "leafletforblazor-tooltip", offset: n.offset }); i.addTo(map), _tooltips_scaling(n, r) } return L.circleMarker(a, void 0 !== l.case.classes.find(t => t.value === o.properties[l.case.field_name]) ? l.case.classes.find(t => t.value === o.properties[l.case.field_name]).symbol : l.default) } }).addTo(map); _layer_scaling(l, n), null !== layers_control && layers_control.addOverlay(n, o) } export function _tooltips_scaling(t, e) { let o = t.stop_with, l = t.start_with; !1 == (0 === l && 1e4 === o) && map.on("zoomend", function () { parseInt(map.getZoom()) >= l && parseInt(map.getZoom()) <= o ? map.addLayer(e) : map.removeLayer(e) }) } export function _layer_scaling(t, e) { let o = 1e4, l = 0; if (null != t && void 0 !== t.scaling && null !== t.scaling) { let a = t.scaling; void 0 !== a.start_with && null !== a.start_with && (l = parseInt(a.start_with)), void 0 !== a.stop_with && null !== a.stop_with && (o = parseInt(a.stop_with)) } !1 == (0 === l && 1e4 === o) && map.on("zoomend", function () { parseInt(map.getZoom()) >= l && parseInt(map.getZoom()) <= o ? map.addLayer(e) : map.removeLayer(e) }) } export function _add_without_any_symbols(t, e, o) { var l = L.geoJSON().addTo(map); let a = 0, n = l.addData(t); for (var i of (null !== layers_control && layers_control.addOverlay(n, o), t)) { if (null != e) { if ("object" == typeof i.geometry && "string" == typeof i.geometry.type) { let r = i.geometry.type.toLowerCase(); if ("point" === r) { let s = tooltip_object(e, i); var p = new L.marker(s.coordinates, { opacity: 0 }); let d = p.bindTooltip(s.content, { permanent: s.permanent, className: "leafletforblazor-tooltip", offset: s.offset }); p.addTo(map), _tooltips_scaling(s, d) } else if ("polygon" === r) { var c = tooltip_object(e, i); let u = L.polygon(i.geometry.coordinates.map(function (t) { return e.coordinate_inversion ? t.map(function (t) { return [t[1], t[0]] }) : t }), { opacity: c.opacity }).bindTooltip(c.content, { permanent: c.permanent }).addTo(map); _tooltips_scaling(c, u) } } if (_is_linestring(t)) { var c = tooltip_object(e, i); let m = L.polyline(c.coordinates, { opacity: c.opacity }).bindTooltip(c.content, { permanent: c.permanent }).addTo(map); _tooltips_scaling(c, m) } } a++ } }