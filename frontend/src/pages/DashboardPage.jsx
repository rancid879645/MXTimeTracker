import { useState, useEffect, useCallback } from 'react'
import LapTimeForm from '../components/LapTimes/LapTimeForm'
import LapTimeTable from '../components/LapTimes/LapTimeTable'
import { getLapTimes, createLapTime, updateLapTime, deleteLapTime } from '../api/lapTimes'

export default function DashboardPage() {
  const [lapTimes, setLapTimes] = useState([])
  const [selected, setSelected] = useState(null)
  const [loadError, setLoadError] = useState('')

  const fetchLapTimes = useCallback(async () => {
    try {
      const data = await getLapTimes()
      setLapTimes(data)
    } catch (err) {
      setLoadError(err.message)
    }
  }, [])

  useEffect(() => { fetchLapTimes() }, [fetchLapTimes])

  const handleSave = async (form) => {
    if (selected) {
      await updateLapTime(selected.id, form)
    } else {
      await createLapTime(form)
    }
    setSelected(null)
    await fetchLapTimes()
  }

  const handleDelete = async (id) => {
    if (!window.confirm('Delete this lap time?')) return
    await deleteLapTime(id)
    await fetchLapTimes()
  }

  return (
    <div className="dashboard">
      <LapTimeForm selected={selected} onSave={handleSave} onCancel={() => setSelected(null)} />
      <hr />
      <h3>Your Lap Times</h3>
      {loadError && <p className="error">{loadError}</p>}
      <LapTimeTable lapTimes={lapTimes} onEdit={setSelected} onDelete={handleDelete} />
    </div>
  )
}
