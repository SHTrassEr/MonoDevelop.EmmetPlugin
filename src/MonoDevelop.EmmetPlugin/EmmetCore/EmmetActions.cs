//  EmmetActions.cs
//
//  Author:
//       SHTrassEr <shtrasser@gmail.com>
//
//  Copyright (c) 2013 SHTrassEr
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace MonoDevelop.EmmetPlugin.EmmetCore
{
    using System;

    /// <summary>
    /// Emmet actions.
    /// </summary>
    public enum EmmetActions
    {
        /// <summary>
        /// Expands CSS-like abbreviations into HTML/XML/CSS code, depending on current document’s syntax. 
        /// Also performs other context actions, for example, transforms CSS Gradient.
        /// </summary>
        ExpandAbbreviation,

        /// <summary>
        /// Takes an abbreviation, expands it and places currently selected content in the last element of 
        /// generated snippet. If there’s no selection, action will silently call “Match Tag Pair” to wrap 
        /// context element.
        /// </summary>
        WrapWithAbbreviation,

        /// <summary>
        /// Quickly removes tag, found by “Match Tag Pair” from current caret position, and adjusts indentation.
        /// </summary>
        RemoveTag,

        /// <summary>
        /// Toggle comment on selection. When there’s no selection, editor’s action toggles comment on current 
        /// line while Emmet’s one do this on current context. For HTML it’s a full tag, for CSS it’s a rule 
        /// or full property.
        /// </summary>
        ToggleComment,

        /// <summary>
        /// Selects next item.
        /// Action is similar to “Go to Edit Point”, but selects important code parts. In HTML, 
        /// these are tag name, full attribute and attribute value. For class attribute it also selects 
        /// distinct classes.
        /// </summary>
        SelectNextItem,

        /// <summary>
        /// Selects previous item.
        /// Action is similar to “Go to Edit Point”, but selects important code parts. In HTML, 
        /// these are tag name, full attribute and attribute value. For class attribute it also selects 
        /// distinct classes.
        /// </summary>
        SelectPreviousItem,

        /// <summary>
        /// A well-known tag balancing: searches for tag or tag's content bounds from current caret 
        /// position and selects it. It will expand (outward balancing) or shrink (inward balancing) selection 
        /// when called multiple times. 
        /// </summary>
        MatchPairOutward,

        /// <summary>
        /// A well-known tag balancing: searches for tag or tag's content bounds from current caret 
        /// position and selects it. It will expand (outward balancing) or shrink (inward balancing) selection 
        /// when called multiple times. 
        /// </summary>
        MatchPairInward,

        /// <summary>
        /// This action splits and joins tag definition, e.g. converts from &lt;tag/&gt; to &lt;tag&gt;&lt;/tag&gt; and 
        /// vice versa. Very useful for XML/XSL developers.
        /// </summary>
        SplitJoinTag,

        /// <summary>
        /// Evaluates simple math expression like 2*4 or 10/2 and outputs its result. You can use \ operator 
        /// which is equivalent to round(a/b).
        /// </summary>
        EvaluateMathExpression,

        /// <summary>
        /// This action works for HTML code blocks and allows you to quickly traverse between important code points:
        /// between tags
        /// empty attributes
        /// newlines with indentation
        /// </summary>
        NextEditPoint,

        /// <summary>
        /// This action works for HTML code blocks and allows you to quickly traverse between important code points:
        /// between tags
        /// empty attributes
        /// newlines with indentation
        /// </summary>
        PrevEditPoint,

        /// <summary>
        /// Merges selected lines into a single one. But when there’s no selection, Emmet will match context HTML tag.
        /// </summary>
        MergeLines,

        /// <summary>
        /// In HTML, allows you to quickly traverse between opening and closing tag
        /// </summary>
        MatchingPair,

        /// <summary>
        /// Takes value of CSS property under caret and copies it into vendor-prefixed variations with 
        /// additional transformations, if required.
        /// </summary>
        ReflectCssValue,

        /// <summary>
        /// Increment number under caret with step 1.
        /// </summary>
        IncrementNumberBy_1,

        /// <summary>
        /// Decrement number under caret with step 1.
        /// </summary>
        DecrementNumberBy_1,

        /// <summary>
        /// Increment number under caret with step 10.
        /// </summary>
        IncrementNumberBy_10,

        /// <summary>
        /// Decrement number under caret with step 10.
        /// </summary>
        DecrementNumberBy_10,

        /// <summary>
        /// Increment number under caret with step 0.1.
        /// </summary>
        IncrementNumberBy_01,

        /// <summary>
        /// Decrement number under caret with step 0.1.
        /// </summary>
        DecrementNumberBy_01
    }
}