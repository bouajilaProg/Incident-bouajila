#!/bin/bash
echo "Creating a new incident with valid data..."
xh post 'http://localhost:5243/api/Incidents' \
  title="chorba" \
  description="stringstri" \
  severity="LOW" \
  status="OPEN"

echo "Creating a new incident with missing title..."
xh post 'http://localhost:5243/api/Incidents' \
  title="Invalid Incident" \
  description="This should fail due to severity" \
  severity="INVALID"
