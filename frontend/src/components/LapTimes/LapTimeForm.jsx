import { useState, useEffect } from 'react'

const empty = { riderName: '', trackName: '', time: '', date: '' }

export default function LapTimeForm({ selected, onSave, onCancel }) {
  const [form, setForm] = useState(empty)
  const [error, setError] = useState('')
  const [loading, setLoading] = useState(false)

  useEffect(() => {
    if (selected) {
      const d = new Date(selected.date)
      setForm({
        riderName: selected.riderName,
        trackName: selected.trackName,
        time: selected.time,
        date: d.toISOString().split('T')[0],
      })
    } else {
      setForm(empty)
    }
  }, [selected])

  const handleChange = (e) => setForm({ ...form, [e.target.name]: e.target.value })

  const handleSubmit = async (e) => {
    e.preventDefault()
    setError('')
    setLoading(true)
    try {
      await onSave({
        riderName: form.riderName,
        trackName: form.trackName,
        time: form.time,
        date: new Date(form.date).toISOString(),
      })
      setForm(empty)
    } catch (err) {
      setError(err.message)
    } finally {
      setLoading(false)
    }
  }

  return (
    <form className="laptime-form" onSubmit={handleSubmit}>
      <h3>{selected ? 'Edit Lap Time' : 'Add Lap Time'}</h3>
      {error && <p className="error">{error}</p>}
      <div className="form-row">
        <input name="riderName" placeholder="Rider Name" value={form.riderName} onChange={handleChange} required />
        <input name="trackName" placeholder="Track Name" value={form.trackName} onChange={handleChange} required />
      </div>
      <div className="form-row">
        <input name="time" placeholder="Lap Time (hh:mm:ss)" value={form.time} onChange={handleChange} required />
        <input name="date" type="date" value={form.date} onChange={handleChange} required />
      </div>
      <div className="form-actions">
        <button type="submit" disabled={loading}>{loading ? 'Saving...' : selected ? 'Update' : 'Add'}</button>
        {selected && <button type="button" className="btn-secondary" onClick={onCancel}>Cancel</button>}
      </div>
    </form>
  )
}
