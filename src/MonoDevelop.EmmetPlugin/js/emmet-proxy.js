var emmet = require("./emmet-full.js");
var sys = require("sys");

var EditorClient = function() {

    var context = null;
    var currentPromptNumber;

    var output = [];

    var pushToOutput = function(action, data) {
        output.push({action: action, data: data});
    };

    this.setContext = function(ctx)
    {
        context = ctx;
        output = [];
        currentPromptNumber = 0;
    };

    this.getOutput = function()
    {
        return output;
    };

    /**
     * Returns current editor's syntax mode
     * @return {String}
     */
    this.getSyntax = function() {
        var m = /\.(\w+)$/.exec(this.getFilePath());
        return emmet.require('actionUtils').detectSyntax(this, m ? m[1].toLowerCase() : null);
    };
    
    /**
     * Returns current output profile name (@see emmet#setupProfile)
     * @return {String}
     */
    this.getProfileName = function() {
        return emmet.require('actionUtils').detectProfile(this);
    };

    /**
     * Returns current caret position
     * @return {Number|null}
     */
    this.getCaretPos = function(){
        return context.caretPos;
    };


    /**
     * Returns editor's content
     * @return {String}
     */
    this.getContent = function(){
        return context.content;
    };

    /**
     * Returns character indexes of selected text: object with <code>start</code>
     * and <code>end</code> properties. If there's no selection, should return 
     * object with <code>start</code> and <code>end</code> properties referring
     * to current caret position
     * @return {Object}
     * @example
     * var selection = editor.getSelectionRange();
     * alert(selection.start + ', ' + selection.end); 
     */
    this.getSelectionRange = function() {
        return context.selectionRange;
    };
    
    /**
     * Returns current line's start and end indexes as object with <code>start</code>
     * and <code>end</code> properties
     * @return {Object}
     * @example
     * var range = editor.getCurrentLineRange();
     * alert(range.start + ', ' + range.end);
     */
    this.getCurrentLineRange = function() {
        return context.currentLineRange;
    };
    
    /**
     * Returns content of current line
     * @return {String}
     */
    this.getCurrentLine = function() {
        return context.currentLine;
    };

    /**
     * Creates selection from <code>start</code> to <code>end</code> character
     * indexes. If <code>end</code> is ommited, this method should place caret 
     * and <code>start</code> index
     * @param {Number} start
     * @param {Number} [end]
     * @example
     * editor.createSelection(10, 40);
     * 
     * //move caret to 15th character
     * editor.createSelection(15);
     */
    this.createSelection = function(start, end) {
        return pushToOutput("createSelection", {start: start, end:end});
    };


    /**
     * Replace editor's content or it's part (from <code>start</code> to 
     * <code>end</code> index). If <code>value</code> contains 
     * <code>caret_placeholder</code>, the editor will put caret into 
     * this position. If you skip <code>start</code> and <code>end</code>
     * arguments, the whole target's content will be replaced with 
     * <code>value</code>. 
     * 
     * If you pass <code>start</code> argument only,
     * the <code>value</code> will be placed at <code>start</code> string 
     * index of current content. 
     * 
     * If you pass <code>start</code> and <code>end</code> arguments,
     * the corresponding substring of current target's content will be 
     * replaced with <code>value</code>. 
     * @param {String} value Content you want to paste
     * @param {Number} [start] Start index of editor's content
     * @param {Number} [end] End index of editor's content
     * @param {Boolean} [no_indent] Do not auto indent <code>value</code>
     */
    this.replaceContent = function(value, start, end, no_indent) {
        return pushToOutput("replaceContent", {value: value, start:start, end:end, no_indent:no_indent});
    };
    /**
     * Set new caret position
     * @param {Number} pos Caret position
     */
    this.setCaretPos = function(pos){
        return pushToOutput("setCaretPos", {pos: pos});
    };
    
    /**
     * Returns current editor's file path
     * @return {String}
     * @since 0.65 
     */
    this.getFilePath = function() {
        return context.filePath;
    }    
    
    /**
    * Ask user to enter something
    * @param {String} title Dialog title
    * @return {String} Entered data
    * @since 0.65
    */
    this.prompt = function(title) {
        if (context.prompts)
        {
            if (currentPromptNumber < context.prompts.length)
            {
                return context.prompts[currentPromptNumber++];
            }
        }
        return '';
    }    
};

var processAction = function(d) {
    d = d.replace(/^\s+|\s+$/g, '');
    if (!d) return;
    //console.error('"' + d + '"');
    var request = JSON.parse(d);
    var action = request.action;
    var context = request.context;
    editor.setContext(context);
    emmet.require('actions').run(action, editor);
    console.log(JSON.stringify(editor.getOutput()));
    //console.error(JSON.stringify(editor.getOutput()));
};


var editor = new EditorClient();
process.stdin.resume();
process.stdin.setEncoding("utf8");
process.stdin.on("data", function(data) {
    processAction(data)
});