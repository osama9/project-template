<template>
    <div>
        <!-- <div>
          <h2>Search and add a pin</h2>
          <label>
            <gmap-autocomplete @place_changed="setPlace"></gmap-autocomplete>
            <button @click="addMarker">Add</button>
          </label>
          <br>
        </div>
        <br>-->
        <input :value="longitude" type="hidden" name="longitude" id="longitude">
        <input :value="latitude" type="hidden" name="latitude" id="latitude">
        <gmap-map @click="clicked" :center="center" :zoom="5.38" style="width:100%;  height: 400px;">
            <gmap-marker :key="index"
                         v-for="(m, index) in markers"
                         :position="m.position"
                         @click="center=m.position"></gmap-marker>
        </gmap-map>
    </div>
</template>

<script>
export default {
  name: "GoogleMap",
  data() {
    return {
      // default to Montreal to keep it simple
      // change this to whatever makes sense
      center: { lat: 23.2256959, lng: 42.3730331 },
      markers: [],
      places: [],
      longitude: "",
      latitude: "",
      currentPlace: null
    };
  },

  mounted() {
    this.geolocate();
  },

  methods: {
    clicked(e) {
      this.currentPlace = {
        geometry: {
          location: {
            lat: e.latLng.lat,
            lng: e.latLng.lng
          }
        }
      };
      this.longitude = e.latLng.lng();
      this.latitude = e.latLng.lat();
      this.markers = [];
      this.addMarker();
    },
    // receives a place object via the autocomplete component
    setPlace(place) {
      this.currentPlace = place;
    },
    addMarker() {
      if (this.currentPlace) {
        const marker = {
          lat: this.currentPlace.geometry.location.lat(),
          lng: this.currentPlace.geometry.location.lng()
        };
        this.markers.push({ position: marker });
        this.places.push(this.currentPlace);
        //this.center = marker;
        this.currentPlace = null;
      }
    },
    geolocate: function() {
      navigator.geolocation.getCurrentPosition(position => {
        this.center = {
          lat: position.coords.latitude,
          lng: position.coords.longitude
        };
      });
    }
  }
};
</script>

<style scoped>
</style>