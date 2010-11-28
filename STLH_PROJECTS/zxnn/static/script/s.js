/* include prototype.js */

function SCOpacity(targetElement)
{
  this.targetElement = targetElement;
  this.h = 1.0;
}

SCOpacity.prototype = {
  initialState: function()
  {
    this.h = 1.0
    with(this.targetElement.style)
    {
      display = 'block';
      opacity = 1.0;
    }
  },
  upgrade: function()
  {
    this.h -= 0.02;
    with(this.targetElement.style)
    {
      display = 'block';
      opacity = this.h;
    }
  },
  relegation: function()
  {
    this.h += 0.02;
    with(this.targetElement.style)
    {
      display = 'block';
      opacity = this.h;
    }
  },
  endState: function()
  {
    this.h = 0.0;
    with(this.targetElement.style)
    {
      display = 'none';
      opacity = 0.0;
    }
  }
}

function ShowEx(sourceElement, stateChanger)
{
  this.sourceElement = sourceElement;
  this.stateChanger = stateChanger;

  sourceElement.showex = this;

  if (sourceElement.addEventListener)
  {
    sourceElement.addEventListener(
      'click',
      function(event)
      {
        event.action = 'click';
        this.showex.handleEvent(event);
      },
      false);
  }
}

ShowEx.prototype = {
  currentState: 'InitialState',
  currentTimer: null,
  currentTicker: null,
  tickerInterval: 80,
  timeout: 500,
  startTimer: function()
  {
    var self = this;
    this.currentTimer = window.setTimeout( function() { self.handleEvent({ action: 'timeout'})},
    this.timeout);
  },
  cancelTimer: function()
  {
    if(this.currentTimer)
    {
      window.clearTimeout(this.currentTimer);
    }
    this.currentTimer = null;
  },
  startTicker: function()
  {
    var self = this;
    this.currentTicker = window.setInterval( function() { self.handleEvent({ action: 'timetick' })},
      this.targetElement);
  },
  cancelTicker: function()
  {
    if(this.currentTicker)
    {
      window.clearInterval(this.currentTicker);
    }
    this.currentTicker = null;
  },
  handleEvent: function(event)
  {
    this.currentState = this.stateFun[this.currentState][event.action].call(this, event);
  },
  stateFun: {
    InitialState: {
      click: function(event) {
        this.startTicker();
        this.startTimer();
        return 'Upgrade';
      }
    },
    Upgrade: {
      timetick: function(event) {
        this.stateChanger.upgrade();
        return this.currentState;
      },
      timeout: function(event) {
        this.cancelTimer();
        this.cancelTicker();
        this.stateChanger.endState();
        return 'EndState';
      }
    },
    EndState: {
      click: function(event) {
        this.startTicker();
        this.startTimer();
        return 'Relegation';
      }
    },
    Relegation: {
      timetick: function(event) {
        this.stateChanger.relegation();
        return this.currentState;
      },
      timeout: function(event) {
        this.cancelTimer();
        this.cancelTicker();
        this.stateChanger.initialState();
        return 'InitialState';
      }
    }
  }
}
