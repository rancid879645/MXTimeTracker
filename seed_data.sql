-- =============================================
-- Motocross Lap Time Tracker — Seed Data
-- Run AFTER create_tables.txt and stored_procedures.txt
--
-- IMPORTANT: BCrypt hashes cannot be computed in plain SQL.
-- To seed a working demo account:
--   1. Start the app and register through the UI (or POST /api/auth/register)
--   2. Note the userId returned in the response
--   3. Update the @UserId value below to match that userId
--   4. Run only the LapTimes INSERT block
--
-- Alternatively, use the pre-hashed block below for jorge_mx / password: Demo1234!
-- The hash was generated with BCrypt work factor 11.
-- =============================================

-- ── Recommended: register through the UI first ─────────────────────────────
-- 1. Start the app (dotnet run + npm run dev)
-- 2. Register at http://localhost:5173 — the response includes { userId, token, username }
-- 3. Copy the userId and replace 'a1b2c3d4-e5f6-7890-abcd-ef1234567890' below
-- 4. Also run the INSERT INTO Users above if you prefer a fixed ID:

INSERT INTO Users (Id, Username, PasswordHash, Email)
VALUES (
    'a1b2c3d4-e5f6-7890-abcd-ef1234567890',
    'jorge_mx',
    -- Replace this with output of: BCrypt.HashPassword("YourPassword")
    -- or register through the UI and skip this INSERT entirely
    '$REPLACE_WITH_REAL_BCRYPT_HASH',
    'jorge@example.com'
);

-- ── Then insert lap times ─────────────────────────────────────────────────
-- Replace the UserId values with the id from your registered user

-- Lap times for jorge_mx
INSERT INTO LapTimes (RiderName, TrackName, Time, Date, UserId)
VALUES ('Jorge Martinez', 'Thunder Valley MX', '00:01:52.340', '2026-05-01 10:00:00', 'a1b2c3d4-e5f6-7890-abcd-ef1234567890');

INSERT INTO LapTimes (RiderName, TrackName, Time, Date, UserId)
VALUES ('Jorge Martinez', 'Thunder Valley MX', '00:01:49.780', '2026-05-03 10:30:00', 'a1b2c3d4-e5f6-7890-abcd-ef1234567890');

INSERT INTO LapTimes (RiderName, TrackName, Time, Date, UserId)
VALUES ('Jorge Martinez', 'Red Bud MX', '00:02:03.120', '2026-05-07 09:00:00', 'a1b2c3d4-e5f6-7890-abcd-ef1234567890');

INSERT INTO LapTimes (RiderName, TrackName, Time, Date, UserId)
VALUES ('Jorge Martinez', 'Red Bud MX', '00:01:58.540', '2026-05-10 09:15:00', 'a1b2c3d4-e5f6-7890-abcd-ef1234567890');

INSERT INTO LapTimes (RiderName, TrackName, Time, Date, UserId)
VALUES ('Jorge Martinez', 'Unadilla MX', '00:02:10.900', '2026-05-15 11:00:00', 'a1b2c3d4-e5f6-7890-abcd-ef1234567890');
