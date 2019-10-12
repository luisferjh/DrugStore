<template>
  <v-card>
    <v-card-title>
      <span class="headline">Nuevo Usuario</span>
    </v-card-title>
    <v-card-text>
      <v-container>
        <v-row>
          <v-col cols="12" sm="6" md="5">
            <v-select
              v-model="role"
              :items="roles"
              item-text="roleName"
              item-value="idRole"
              label="Rol*"
              required
            ></v-select>                         
          </v-col>
          <v-col cols="12" sm="6" md="12">
            <v-text-field label="Nombre de usuario*" v-model="userName" required></v-text-field>
          </v-col>                                

          <v-col cols="12">
            <v-text-field label="Tipo de documento" v-model="documentType"></v-text-field>
          </v-col>                    

            <v-col cols="12">
            <v-text-field label="Numero de documento" v-model="documentNumber" ></v-text-field>
          </v-col>
          
          <v-col cols="12" md="12">
            <v-text-field label="Direccion" v-model="address" required></v-text-field>
          </v-col>  

          <v-col cols="12">
            <v-text-field label="Numero de telefono" v-model="phoneNumber"></v-text-field>
          </v-col>
          
          <v-col cols="12" md="12">
            <v-text-field 
            label="Contraseña*" 
            v-model="password" 
            :append-icon="show1 ? 'visibility' : 'visibility_off'" 
            :type="show1 ? 'text' : 'password'"
            @click:append="show1 = !show1"
            required
            >
            </v-text-field>
          </v-col>  

          <v-col cols="12" md="12">
            <v-text-field label="Email*" v-model="email" required></v-text-field>
          </v-col>  

          <v-col cols="12">
          
            <v-select
              v-model="estado"
              :items="[{'estado':'Activo', 'value':true}, {'estado':'Inactivo', 'value':false}]"   
              item-text="estado"
              item-value="value"                       
              label="Estado*"
              required
            ></v-select>  
          </v-col>
        </v-row>
      </v-container>
      <small>*indica campo obligatorio</small>
      
    </v-card-text>
    <v-card-actions>
      <div class="flex-grow-1"></div>
      <v-btn color="blue darken-1" text @click="cancel()">Cerrar</v-btn>
      <v-btn color="blue darken-1" text @click="save()">Guardar</v-btn>
    </v-card-actions>
  </v-card> 
</template>

<script>
  export default {    
    props: {          
      roles:{
        type:Array,
        required:true
      },      
    },
    data() {
      return {
        role:'',     
        userName:'',
        documentType:'',
        documentNumber:'',
        phoneNumber:'', 
        address:'',
        email:'',
        password:'',       
        estado:'',       
        menu1: false,
        show1:false
      }
    },
    methods: {
      save() {
        this.$emit('save',{           
          'idRole':this.role,
          'userName':this.userName,
          'documentType':this.documentType,    
          'documentNumber':this.documentNumber,  
          'address':this.address,                  
          'phoneNumber':this.phoneNumber,    
          'email':this.email,
          'password':this.password,      
          'condition':this.estado
        })
      },
      cancel(){
        this.$emit('cancel',false)
      }
    },
  }
</script>

