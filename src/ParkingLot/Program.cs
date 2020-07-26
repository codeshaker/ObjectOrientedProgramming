using System;

namespace ParkingLot
{
	enum AccountStatus {};
	enum ParkingTicketStatus {PAID, UNPAID, LOST};
	enum ParkingSpotType {COMPACT, HANDICAPPED};
	enum VehicleType {CAR, BIKE};
	
	public class Address {}

	public class Person {}

	pubic class Account {
		string uid, password;
		Person person;
		AccountStatus AccountStatus;
	}

	public class Admin extends Account {}

	public class ParkingAttendant extends Account {}

	public class ParkingSpot {
		int no;
		bool isoccupied;
		final ParkingSpotType type;
		Vehicle vehicle;

		ParkingSpot(ParkingSpotType type) {
			this.type=type;
		}

		AssignVehicle(Vehicle v){
			this.vehicle=v;
			this.free=false;
		}

		RemoveVehicle(Vehicle v){
			this.vehicle=null
			this.free=true;
		}
	}

	public class CompactSpot extends ParkingSpot {
		public CompactSpot(ParkingSpotType.COMPACT) {
			super(ParkingSpotType.COMPACT);
		}
	}

	public class HandicappedSpot extends ParkingSpot {}

	public class ParkingTicket {}

	public class Vehicle {
		string licenseno;
		VehicleType type;
		ParkingTicket ticket;
	}

	public class Car extends Vehicle {
		public Car(VehicleType.CAR) {
			SUPER(VehicleType.CAR);
		}
	}

	public class ParkingDispalyBord {
		int id;
		Compact freeCompactSpot;
		Handicapped freeHandicapped;

		public void showEmptySpot() {}
	}

	public class ParkingFloor{
		int no;
		map<string, HandicappedSpot> HandicappedSpots;
		map<string, CompactSpot> CompactSpots;
		ParkingDispalyBord displayboard;

		public addParkingSpot(ParkingSpot spot)
		{
			switch(spot.getType)
			{
				case ParkingSpotType.COMPACT:
				CompactSpots.insert(spot.getid(), spot);
				break;
			}
		}

		public bool AssignVehicleToSpot(ParkingSpot spot, vehicle vehicle) {

			if (spot.isoccupied)
			{
				return false;
			}

			spot.AssignVehicle(vehicle);

			switch(spot.getType)
			{
				case ParkingSpotType.COMPACT:
				UpdateDisplayBoardForComapct(ParkingSpotType.COMPACT);
				break;
			}
		}

		public bool UpdateDisplayBoardForComapct(ParkingSpot spot) {

			if (this.ParkingDispalyBord.getfreeCompactSpot().getid() == spot.getid())
			{
				for (KeyValuePair<string, HandicappedSpot> idx in CompactSpots)
				{
					string key=idx.first;
					CompactSpot value=idx.second;
					if (!value.isoccupied)
					{
						this.ParkingDispalyBord.setFreeCompact(value);
					}
				}
			}

			this.ParkingDispalyBord.showEmptySpot();
		}

		public bool freeSpot(ParkingSpot spot) {
			this.spot.RemoveVehicle();
		}
	}

	public class EntracePanel {}
	public class ExitPanel {}

	public class ParkingLot {
		int id;
		Address Address;
		string name;
		ParkingRate rate;

		int totalhandicappedSpotCount;
		int totalcompactSpotCount;
		int totalargeSpotCount;


		int HandicappedSpotCount, CompactSpotCount, largeSpotCount;

		map<string, ParkingFloor> ParkingFloors;
		map<string, ExitPanel> ExitPanels;
		map<string, EntracePanel> EntracePanel;
		map<string, ParkingTicket> ActiveTickets;

		// singleton class
		private static ParkingLot parkinglot = null;

		private ParkingLot() {
			// assign name
			// from DB assign parking floors, entrace and exit panels.
		}

		private static ParkingLot getInstance() {
			if (parkinglot == null) {
				parkinglot = new ParkingLot();
			}
		}

		private synchronize ParkingTicket getNewParkingTicket(Vehicle vehicle) throws ParkingFullException {

			if (this.isFull(vehicle.getType()))
			{
				throw new ParkingFullException();
			}

			ParkingTicket ticket = new ParkingTicket();
			ticket.assign(vehicle);
			ticket.saveDB();

			incrementSpotCount(vehicle.getType());
			ActiveTickets.insert(ticket.getid(), ticket);
			return ticket;

		}


		public bool isFull(VehicleType type)
		{
			if (type == VehicleType.TRUCk || type == VehicleType.VAN)
			{
				return largeSpotCount >= totalargeSpotCount;
			}
		}



	}
