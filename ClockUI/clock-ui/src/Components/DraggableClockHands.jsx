import React, { useRef, useState } from 'react';

function DraggableClockHands({ hour, minute, onHourChange, onMinuteChange }) {
  const clockRef = useRef(null);
  const [dragging, setDragging] = useState(null); // 'hour' or 'minute'

  const handleMouseMove = (event) => {
    if (!dragging) return; // Only drag if a hand is selected

    const rect = clockRef.current.getBoundingClientRect();
    const centerX = rect.width / 2;
    const centerY = rect.height / 2;
    const x = event.clientX - rect.left - centerX;
    const y = event.clientY - rect.top - centerY;

    const angle = Math.atan2(y, x);
    if (dragging === 'hour') {
      let hours = (angle + Math.PI) / (Math.PI * 2) * 12;
      if (hours < 0) hours += 12;
      onHourChange(Math.floor(hours));
    } else if (dragging === 'minute') {
      let minutes = (angle + Math.PI) / (Math.PI * 2) * 60;
      if (minutes < 0) minutes += 60;
      onMinuteChange(Math.floor(minutes));
    }
  };

  const handleMouseUp = () => {
    setDragging(null); // Stop dragging when mouse is released
  };

  const handleHandClick = (hand) => {
    setDragging(hand); // Set which hand is being dragged
  };

  // Function to calculate the position of clock numbers
  const getNumberPosition = (number, radius, angleOffset) => {
    const angle = (number * 30 - angleOffset) * (Math.PI / 180); // Convert degrees to radians
    const x = 100 + radius * Math.cos(angle);
    const y = 100 + radius * Math.sin(angle);
    return { x, y };
  };

  return (
    <div
      ref={clockRef}
      onMouseMove={handleMouseMove}
      onMouseUp={handleMouseUp}
      style={{ position: 'relative', width: '200px', height: '200px' }}
    >
      <svg width="200" height="200">
        {/* Clock Face */}
        <circle cx="100" cy="100" r="80" fill="#fff" stroke="#000" strokeWidth="2" />

        {/* Clock Numbers */}
        {Array.from({ length: 12 }, (_, i) => {
          const number = i + 1;
          const { x, y } = getNumberPosition(number, 70, 90); // Position numbers at 70px radius
          return (
            <text
              key={number}
              x={x}
              y={y}
              textAnchor="middle"
              alignmentBaseline="middle"
              fill="#000"
              fontSize="14"
              fontWeight="bold"
            >
              {number}
            </text>
          );
        })}

        {/* Hour Hand */}
        <line
          x1="100"
          y1="100"
          x2="100"
          y2="50"
          stroke="#000"
          strokeWidth="8"
          transform={`rotate(${360 / 12 * hour}, 100, 100)`}
          onClick={() => handleHandClick('hour')}
          style={{ cursor: 'pointer' }} // Show pointer cursor on hover
        />

        {/* Minute Hand */}
        <line
          x1="100"
          y1="100"
          x2="100"
          y2="20"
          stroke="#000"
          strokeWidth="5"
          transform={`rotate(${360 / 60 * minute}, 100, 100)`}
          onClick={() => handleHandClick('minute')}
          style={{ cursor: 'pointer' }} // Show pointer cursor on hover
        />
      </svg>
    </div>
  );
}

export default DraggableClockHands;