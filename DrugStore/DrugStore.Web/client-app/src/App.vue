<template>
  <v-app>
    <AppNavigation v-if="loggedIn" />    
    <v-content>
      <router-view></router-view>
    </v-content>
  </v-app>
</template>

<script>
import AppNavigation from './components/AppNavigation';
export default {
  name: 'App',
  components: {
    AppNavigation
  },
  data() {
    return {
     
    };
  },
  computed: {
    loggedIn() {
      return this.$store.state.user
    }
  },
};
</script>
