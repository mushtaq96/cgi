import React, { useState, useEffect } from 'react';
import DraggableClockHands from './Components/DraggableClockHands';
import Clock from './Components/Clock';

function App() {
  const [hour, setHour] = useState(3); // Initialize hour to 3 (3 o'clock)
  const [minute, setMinute] = useState(0); // Initialize minute to 0 (00 minutes)
  const [hourDragged, setHourDragged] = useState(false); // Track if hour hand is dragged
  const [minuteDragged, setMinuteDragged] = useState(false); // Track if minute hand is dragged
  const [apiResponse, setApiResponse] = useState(null); // Store API response
  const [showInstructions, setShowInstructions] = useState(true); // Show instructions on first load

  const handleHourChange = (newHour) => {
    setHour(newHour);
    setHourDragged(true); // Mark hour hand as dragged
  };

  const handleMinuteChange = (newMinute) => {
    setMinute(newMinute);
    setMinuteDragged(true); // Mark minute hand as dragged
  };

  const sendTimeToBackend = () => {
    const fHour = Math.round(hour); // Round hour to the nearest integer
    console.log('Sending time to backend:', fHour, minute);

    // Call the BE API
    fetch(`http://localhost:5140/api/clock/hands?hours=${fHour}&minutes=${minute}`)
      .then(response => {
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        return response.json();
      })
      .then(data => {
        console.log('API response:', data);
        setApiResponse(data); // Store the API response
      })
      .catch(error => {
        console.error('Error:', error);
        setApiResponse({ error: error.message }); // Store the error message
      });
  };

  // Hide instructions when the user interacts with the clock
  useEffect(() => {
    if (hourDragged || minuteDragged) {
      setShowInstructions(false);
    }
  }, [hourDragged, minuteDragged]);

  return (
    <div className="center-container">
      <div className="content">
        <h1>Interactive Clock</h1>
        <div className="clock-container">
          <DraggableClockHands
            hour={hour}
            minute={minute}
            onHourChange={handleHourChange}
            onMinuteChange={handleMinuteChange}
          />
          {/* Display the selected time only if both hands have been dragged */}
          {hourDragged && minuteDragged && (
            <div style={{ margin: '20px 0', fontSize: '24px', fontWeight: 'bold' }}>
              Selected Time: {Math.round(hour)}:{minute.toString().padStart(2, '0')}
            </div>
          )}
          <button onClick={sendTimeToBackend}>Convert to Angles</button>
        </div>

        {/* Display API response (angles) */}
        {apiResponse && (
          <div style={{ margin: '20px 0', fontSize: '18px' }}>
            <strong>Result:</strong>
            <pre>
              Hour Angle: {apiResponse.hour}°
              <br />
              Minute Angle: {apiResponse.minute}°
            </pre>
          </div>
        )}

        {/* Instructions Modal */}
        {showInstructions && (
          <div className="instructions-modal">
            <div className="instructions-content">
              <h2>How to Use</h2>
              <p>Click on either the <strong>hour</strong> or <strong>minute</strong> hand and drag your mouse to set the time.</p>
              <button onClick={() => setShowInstructions(false)}>Got it!</button>
            </div>
          </div>
        )}
      </div>
    </div>
  );
}

export default App;