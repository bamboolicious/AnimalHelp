using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Timeline;

namespace UnityEditor.Timeline
{
    static class SelectionManager
    {
        static IClipCurveEditorOwner currentInlineEditorCurve;

        public static void AddObject(Object obj)
        {
            if (obj == null)
                return;

            currentInlineEditorCurve = null;
            if (Selection.Contains(obj))
                return;

            Selection.Add(obj);
        }

        public static void Add(TrackAsset obj)
        {
            // this can indicate a deleted track (Unity 'null')
            if (obj == null)
                return;

            currentInlineEditorCurve = null;
            if (!Selection.Contains(obj))
            {
                WindowState state = null;
                if (TimelineWindow.instance != null)
                    state = TimelineWindow.instance.state;

                if (!Selection.instanceIDs.Any() && state != null && state.editSequence.director != null)
                    Selection.SetActiveObjectWithContext(obj, TimelineWindow.instance.state.editSequence.director);
                else
                    Selection.Add(obj);
            }
        }

        public static void Add(TimelineClip item)
        {
            AddObject(EditorClipFactory.GetEditorClip(item));
        }

        public static void Add(IMarker marker)
        {
            var markerAsObject = marker as Object;
            if (markerAsObject != null)
            {
                if (!Selection.Contains(markerAsObject))
                {
                    currentInlineEditorCurve = null;
                    WindowState state = null;
                    if (TimelineWindow.instance != null)
                        state = TimelineWindow.instance.state;

                    if (!Selection.instanceIDs.Any() && state != null && state.editSequence.director != null)
                        Selection.SetActiveObjectWithContext(markerAsObject, TimelineWindow.instance.state.editSequence.director);
                    else
                        Selection.Add(markerAsObject);
                }
            }
        }

        public static void Add(ITimelineItem item)
        {
            var clipItem = item as ClipItem;
            if (clipItem != null)
            {
                Add(clipItem.clip);
            }
            else
            {
                var markerItem = item as MarkerItem;
                if (markerItem != null)
                {
                    Add(markerItem.marker);
                }
            }
        }

        public static void SelectOnly(TrackAsset track)
        {
            RemoveTimelineSelection();
            Add(track);
        }

        public static void SelectOnly(TimelineClip clip)
        {
            RemoveTimelineSelection();
            Add(clip);
        }

        public static void SelectOnly(IMarker item)
        {
            RemoveTimelineSelection();
            Add(item);
        }

        public static void SelectOnly(ITimelineItem item)
        {
            var clipItem = item as ClipItem;
            if (clipItem != null)
            {
                SelectOnly(clipItem.clip);
            }
            else
            {
                var markerItem = item as MarkerItem;
                if (markerItem != null)
                {
                    SelectOnly(markerItem.marker);
                }
            }
        }

        public static void SelectInlineCurveEditor(IClipCurveEditorOwner selection)
        {
            // case 1030681 - selecting an inline curve should remove clips and
            // tracks to prevent shortcuts being forwarded incorrectly
            if (selection != null && selection != currentInlineEditorCurve)
            {
                var track = selection.owner;
                var selectedTracks = SelectedTracks().ToArray();
                var selectedClips = SelectedClips().ToArray();

                foreach (var t in selectedTracks)
                {
                    if (t != track)
                        Remove(t);
                }

                foreach (var c in selectedClips)
                {
                    if (c.parentTrack != track)
                        Remove(c);
                }
            }


            currentInlineEditorCurve = selection;
        }

        public static IClipCurveEditorOwner GetCurrentInlineEditorCurve()
        {
            return currentInlineEditorCurve;
        }

        public static bool IsCurveEditorFocused(IClipCurveEditorOwner selection)
        {
            return selection == currentInlineEditorCurve;
        }

        public static bool Contains(TrackAsset item)
        {
            if (item == null)
                return false;

            return Selection.Contains(item);
        }

        public static bool Contains(TimelineClip item)
        {
            if (item == null)
                return false;
            return Selection.Contains(EditorClipFactory.GetEditorClip(item));
        }

        public static bool Contains(Object obj)
        {
            return Selection.Contains(obj);
        }

        public static bool Contains(IMarker marker)
        {
            var markerAsObject = marker as Object;
            return Selection.Contains(markerAsObject);
        }

        public static bool Contains(ITimelineItem item)
        {
            var clipItem = item as ClipItem;
            if (clipItem != null)
            {
                return Contains(clipItem.clip);
            }

            var markerItem = item as MarkerItem;
            return markerItem != null && Contains(markerItem.marker);
        }

        public static void Clear()
        {
            currentInlineEditorCurve = null;
            Selection.activeObject = null;
        }

        public static void UnSelectTracks()
        {
            var newSelection = Selection.objects.Where(x => !(x is TrackAsset));
            Selection.objects = newSelection.ToArray();
        }

        public static void Remove(TimelineClip item)
        {
            Remove(EditorClipFactory.GetEditorClip(item));
        }

        public static void Remove(IMarker marker)
        {
            var o = marker as Object;
            if (o != null)
            {
                Remove(o);
            }
        }

        static void Remove(Object item)
        {
            Selection.Remove(item);
        }

        public static void Remove(TrackAsset item)
        {
            Selection.Remove(item);
        }

        public static void Remove(ITimelineItem item)
        {
            var clipItem = item as ClipItem;
            if (clipItem != null)
            {
                Remove(clipItem.clip);
            }
            else
            {
                var markerItem = item as MarkerItem;
                if (markerItem != null)
                {
                    Remove(markerItem.marker);
                }
            }
        }

        public static void RemoveTimelineSelection()
        {
            Selection.objects = Selection.objects.Where(s => !IsTimelineType(s)).ToArray();
        }

        public static void RemoveAllClips()
        {
            Selection.objects = Selection.objects.Where(s => !(s is EditorClip)).ToArray();
        }

        public static int Count()
        {
            return Selection.objects.Count(IsTimelineType);
        }

        public static IEnumerable<TimelineClipGUI> SelectedClipGUI()
        {
            foreach (var clip in SelectedClips())
            {
                var gui = ItemToItemGui.GetGuiForClip(clip);
                if (gui != null)
                    yield return gui;
            }
        }

        public static IEnumerable<TimelineClip> SelectedClips()
        {
            return Selection.objects.OfType<EditorClip>().Select(x => x.clip);
        }

        public static IEnumerable<IMarker> SelectedMarkers()
        {
            return Selection.objects.OfType<IMarker>();
        }

        public static IEnumerable<TrackAsset> SelectedTracks()
        {
            ‹xdAPEØP‹‹p‹Îÿ„u‹ÏÿÖ‹Èèl8ÿÿƒMüÿMØènüÿ‹lÿÿÿè@úÿÿ‹ø‹‹q‹Îÿ„u‹ÏÿÖƒx †8  ‹lÿÿÿèúÿÿ‹ø‹‹q‹Îÿ„u‹ÏÿÖƒeĞ j‰…pÿÿÿX‰EÔÆEÀ ‹lÿÿÿ‰Eüèãùÿÿ‹ø‹‹q‹Îÿ„u‹ÏÿÖMÀ‹@Áà
Pè‘ıÿ‹µpÿÿÿ…tÿÿÿP‹ÎÇ…tÿÿÿ   èÈ2şÿ‹>;Çtƒx~‹Ç;ø„ì   hd¬èı?
 YPhd¬MÀè—jüÿEØjPè¬ñüÿEØÆEüPEh8ÜPè¢_ıÿƒÄƒx‹ÈÆEür‹ÿpQMÀèYjüÿMèmüÿMØÆEüèõlüÿ¿`Wè‘?
 YPWMÀè/jüÿ¿dWè{?
 YPWMÀèjüÿ…tÿÿÿÇ…tÿÿÿ   P…dÿÿÿ‹ÎPèı÷ÿÿ‹…dÿÿÿƒx(Hr‹Hÿp$QMÀèÜiüÿ¿d¬Wè(?
 YPWMÀèÆiüÿ‹>…tÿÿÿÇ…tÿÿÿ   P‹Îè®1şÿ;Çtƒx~‹Ç;ø„  ƒ}Ğ thhèß>
 YPhhMÀèyiüÿ¿d¬WèÅ>
 YPWMÀèciüÿEØjPèxğüÿEØÆEüPEh8ÜPèn^ıÿƒÄƒx‹ÈÆEür‹ÿpQMÀè%iüÿMèÍküÿMØÆEüèÁküÿ¿`Wè]>
 YPWMÀèûhüÿ¿lWèG>
 YPWMÀèåhüÿ…tÿÿÿÇ…tÿÿÿ   P…dÿÿÿ‹ÎPèÉöÿÿ‹…dÿÿÿƒx(Hr‹Hÿp$QMÀè¨hüÿ¿d¬Wèô=
 YPWMÀè’hüÿë¿d¬…tÿÿÿÇ…tÿÿÿ   P‹Îèu0şÿ;tƒx~‹9„  ƒ}Ğ thhè¦=
 YPhhMÀè@hüÿWè‘=
 YPWMÀè/hüÿEØjPèDïüÿEØÆEüPEh8ÜPè:]ıÿƒÄƒx‹ÈÆEür‹ÿpQMÀèñgüÿMè™jüÿMØÆEüèjüÿ¿`Wè)=
 YPWMÀèÇgüÿ¿xWè=
 YPWMÀè±güÿ…tÿÿÿÇ…tÿÿÿ   P…dÿÿÿ‹ÎPè•õÿÿ‹…dÿÿÿƒx(Hr‹Hÿp$QMÀètgüÿ¾d¬VèÀ<
 YPVMÀè^güÿƒ}Ğ t>h|MØèĞgüÿ‹ÆEü‹xdEÀPEØP‹‹p‹Îÿ„u‹ÏÿÖ‹Èè4ÿÿMØèÊiüÿƒMüÿMÀè¾iüÿ‹lÿÿÿèàõÿÿ‹ø‹‹q‹Îÿ„u‹ÏÿÖ<uZhP¦MÀèdgüÿh„MØÇEü   èPgüÿ‹ÆEü‹xdEÀPEØP‹‹p‹Îÿ„u‹ÏÿÖ‹Èè”3ÿÿMØèJiüÿMÀèBiüÿƒe¸ ÇE¼   ÆE¨ ‹M¨ÇEü   ‹@8kÀKPèŒıÿ‹3‹v4‹‰…pÿÿÿë:ƒ}¸ xt
j,M¨è,ÿüÿƒ‹Çr‹ÿwM¨Pè3füÿpÿÿÿèF üÿ‹…pÿÿ