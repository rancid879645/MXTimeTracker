export default function LapTimeTable({ lapTimes, onEdit, onDelete }) {
  if (lapTimes.length === 0)
    return <p className="empty-state">No lap times recorded yet. Add your first one above.</p>

  return (
    <div className="table-wrapper">
      <table>
        <thead>
          <tr>
            <th>Rider</th>
            <th>Track</th>
            <th>Time</th>
            <th>Date</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {lapTimes.map((lt) => (
            <tr key={lt.id}>
              <td>{lt.riderName}</td>
              <td>{lt.trackName}</td>
              <td>{lt.time}</td>
              <td>{new Date(lt.date).toLocaleDateString()}</td>
              <td className="actions">
                <button className="btn-edit" onClick={() => onEdit(lt)}>Edit</button>
                <button className="btn-delete" onClick={() => onDelete(lt.id)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}
